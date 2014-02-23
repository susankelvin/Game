using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace BestGame
{
    public class Engine
    {
        private const int NativeTimeOut = 50;
        private const int UpdateEnemiesPasses = 5;
        private const int CreationPasses = 16;
        private readonly int consoleWidth;
        private readonly int consoleHeight;
         
        private ulong Counter { get; set; }
        private Random Random { get; set; }
        private List<IWeapon> PlayerShots { get; set; }
        private List<IWeapon> EnemyShots { get; set; }
        private IPlayer Player { get; set; }
        private List<IMoveable> GameObjects { get; set; }
        private BoundsRect BattleArena { get; set; }
        private StatusBar StatusBar { get; set; }
        private bool PlayerKilled { get; set; }

        public Engine(int consoleWidth, int consoleHeight)
        {
            this.consoleWidth = consoleWidth;
            this.consoleHeight = consoleHeight;
            this.PlayerShots = new List<IWeapon>();
            this.EnemyShots = new List<IWeapon>();
            this.GameObjects = new List<IMoveable>();
            this.Counter = 0;
            this.Random = new Random();
            this.StatusBar = new StatusBar();
            this.BattleArena = new BoundsRect(0, 0, consoleWidth - 1,  consoleHeight - this.StatusBar.BoundsRect.Height - 1);
            this.StatusBar.SetPostion(new Vector(0, this.BattleArena.Height));
            this.Player = new Player(new Vector(consoleWidth / 2, this.BattleArena.Height - 1));

            if (this.StatusBar.ScoreUpdate != null)
            {
                this.Player.ScoreUpdate += this.StatusBar.ScoreUpdate;
            }

            if (this.StatusBar.WeaponChange != null)
            {
                this.Player.WeaponChange += this.StatusBar.WeaponChange;
            }

            this.Player.AddWeapon(Weapons.Missile);
            this.Player.SelectWeapon(2);
            this.PlayerKilled = false;
        }

        private void InitConsole()
        {
            Console.BufferWidth = consoleWidth + 1;
            Console.WindowWidth = consoleWidth + 1;
            Console.BufferHeight = consoleHeight;
            Console.WindowHeight = consoleHeight;
            Console.CursorVisible = false;
            Console.Title = "Best game";
            Console.OutputEncoding = Encoding.Unicode;
        }

        private void DrawBackground()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
        }

        private void DrawShots()
        {
            foreach (IWeapon shot in PlayerShots)
            {
                shot.Draw();
            }

            foreach (IWeapon shot in EnemyShots)
            {
                shot.Draw();
            }
        }

        private void DrawObjects()
        {
            foreach (IDrawable item in GameObjects)
            {
                item.Draw();
            }
        }

        private bool ReadKeyboard()
        {
            ConsoleKeyInfo key;

            while (Console.KeyAvailable)
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.Escape)
                {
                    return false;
                }

                this.ProcessKey(key);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                        continue;
                    default:
                        return true;
                }
            }

            return true;
        }

        public void Run()
        {
            this.InitConsole();
            this.PrintWelcomeMessage();

            do
            {
                this.DrawBackground();
                this.StatusBar.Draw();
                this.Player.Draw();
                this.DrawShots();
                this.DrawObjects();

                this.Counter++;
                Thread.Sleep(NativeTimeOut);

                if (!ReadKeyboard())
                {
                    break;
                }

                if (this.Counter % CreationPasses == 0)
                {
                    AddEnemy();
                }

                if (this.Counter % UpdateEnemiesPasses == 0)
                {
                    this.MoveObjects();
                    this.NotifyShotTime();
                }

                this.MoveShots();
            } while (!this.PlayerKilled);

            Console.ResetColor();
            Console.Clear();
            this.PrintScore();
            while (!Console.KeyAvailable)
            { }
        }

        private bool TryDestroy(IMoveable item)
        {
            if (item is Rock)
            {
                return false;
            }
            IDestructable enemy = item as IDestructable;

            if ((item != null) && (enemy.Shield <= 0))
            {
                this.GameObjects.Remove(item);
                return true;
            }

            return false;
        }

        private void HandleShotHit(IWeapon weapon, IMoveable moveable)
        {
            IDestructable enemy = moveable as IDestructable;

            if (enemy != null)
            {
                this.Player.Score += weapon.Attack(enemy);
            }
        }

        private void MoveShots()
        {
            BoundsRect bounds;
            List<int> shotsToRemove = new List<int>();
            bool shotDestroyed;

            for (int i = 0; i < PlayerShots.Count; i++)
            {
                bounds = this.PlayerShots[i].Advance();

                if (bounds.LiesInside(BattleArena))
                {
                    this.PlayerShots[i].Move(bounds.TopLeft);
                    shotDestroyed = false;

                    // Check enemy object hit.
                    for (int j = 0; j < this.GameObjects.Count; j++)
                    {
                        if (bounds.Intersects(this.GameObjects[j].BoundsRect))
                        {
                            if(this.PlayerShots[i] is Missile)
                            {
                                BoundsRect explodeRadius = new BoundsRect(bounds.Left - 10, bounds.Top - 10, bounds.Right + 10, bounds.Bottom + 10);
                                MissileExplode(explodeRadius, this.PlayerShots[i]);
                                shotsToRemove.Add(i);
                                shotDestroyed = true;
                                break;
                            }
                            else
                            {
                                HandleShotHit(this.PlayerShots[i], this.GameObjects[j]);
                                TryDestroy(this.GameObjects[j]);
                                shotsToRemove.Add(i);
                                shotDestroyed = true;
                                break;
                            }
                        }
                    }

                    if (shotDestroyed)
                    {
                        continue;
                    }

                    // Check enemy shot hit.
                    for (int j = 0; j < this.EnemyShots.Count; j++)
                    {
                        if (bounds.Intersects(this.EnemyShots[j].BoundsRect))
                        {
                            this.EnemyShots.RemoveAt(j);
                            shotsToRemove.Add(i);
                            break;
                        }
                    }
                }
                else
                {
                    shotsToRemove.Add(i);
                }
            }

            CleanList(this.PlayerShots, shotsToRemove);
            shotsToRemove.Clear();

            for (int i = 0; i < this.EnemyShots.Count; i++)
            {
                bounds = this.EnemyShots[i].Advance();

                if (bounds.LiesInside(BattleArena))
                {
                    this.EnemyShots[i].Move(bounds.TopLeft);

                    if (this.EnemyShots[i].BoundsRect.Intersects(Player.BoundsRect))
                    {
                        this.PlayerKilled = true;
                        return;
                    }

                    for (int j = 0; j < this.GameObjects.Count; j++)
                    {
                        if (bounds.Intersects(this.GameObjects[j].BoundsRect))
                        {
                            shotsToRemove.Add(i);
                            break;
                        }
                    }
                }
                else
                {
                    shotsToRemove.Add(i);
                }
            }

            CleanList(this.EnemyShots, shotsToRemove);
        }

        private void MissileExplode(BoundsRect missileRadius, IWeapon weapon)
        {
            for (int j = 0; j < this.GameObjects.Count; j++)
            {
                if (this.GameObjects[j].BoundsRect.LiesInside(missileRadius))
                {
                    HandleShotHit(weapon, this.GameObjects[j]);
                    TryDestroy(this.GameObjects[j]);
                }
            }
        }

        private void MoveObjects()
        {
            IMoveable moveable;
            BoundsRect bounds;
            List<int> forRemoval = new List<int>();
            bool objectDestroyed;
            IDestructable enemy;

            for (int i = 0; i < this.GameObjects.Count; i++)
            {
                moveable = this.GameObjects[i];
                bounds = moveable.Advance();

                if (bounds.LiesInside(this.BattleArena))
                {
                    // Check if object hits the player.
                    if (this.Player.BoundsRect.Intersects(bounds))
                    {
                        this.PlayerKilled = true;
                        return;
                    }

                    // Check if object collides with player's shot.
                    objectDestroyed = false;
                    for (int j = 0; j < this.PlayerShots.Count; j++)
                    {
                        if (bounds.Intersects(this.PlayerShots[j].BoundsRect))
                        {
                            HandleShotHit(this.PlayerShots[j], moveable);
                            this.PlayerShots.RemoveAt(j);
                            enemy = moveable as IDestructable;

                            if ((enemy != null) && (enemy.Shield <= 0))
                            {
                                forRemoval.Add(i);
                            }

                            objectDestroyed = true;
                            break;
                        }
                    }

                    if (objectDestroyed)
                    {
                        continue;
                    }

                    // Check if object collides witn enemy shots.
                    for (int j = 0; j < this.EnemyShots.Count; j++)
                    {
                        if (bounds.Intersects(this.EnemyShots[j].BoundsRect))
                        {
                            this.EnemyShots.RemoveAt(j);
                            break;
                        }
                    }

                    moveable.Move(bounds.TopLeft);
                }
                else
                {
                    forRemoval.Add(i);
                }
            }

            CleanList(GameObjects, forRemoval);
        }

        private void NotifyShotTime()
        {
            IOffencible badEnemy;

            foreach (var enemy in this.GameObjects)
            {
                badEnemy = enemy as IOffencible;

                if (badEnemy != null)
                {
                    this.EnemyShots.AddRange(badEnemy.Shoot());
                }
            }
        }

        private void ProcessKey(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.Spacebar:
                    PlayerShots.AddRange(Player.Shoot());
                    break;
                case ConsoleKey.LeftArrow:
                    if (this.Player.BoundsRect.Left > 0)
                    {
                        this.Player.Move(new Vector(this.Player.BoundsRect.Left - 1,
                            this.Player.BoundsRect.Top));
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (this.Player.BoundsRect.Left + this.Player.BoundsRect.Width < consoleWidth)
                    {
                        this.Player.Move(new Vector(this.Player.BoundsRect.Left + 1,
                            this.Player.BoundsRect.Top));
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (this.Player.BoundsRect.Top > this.BattleArena.Height * 2 / 3)
                    {
                        this.Player.Move(new Vector(this.Player.BoundsRect.Left,
                            this.Player.BoundsRect.Top - 1));
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (this.Player.BoundsRect.Bottom < this.BattleArena.Height - 1)
                    {
                        this.Player.Move(new Vector(this.Player.BoundsRect.Left,
                            this.Player.BoundsRect.Top + 1));
                    }
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                case ConsoleKey.D4:
                case ConsoleKey.D5:
                case ConsoleKey.D6:
                case ConsoleKey.D7:
                case ConsoleKey.D8:
                case ConsoleKey.D9:
                    this.Player.SelectWeapon(consoleKeyInfo.KeyChar - '0');
                    break;
            }
        }

        private void AddEnemy()
        {
            Enemies newEnemyKind = (Enemies)Random.Next(Enum.GetValues(typeof(Enemies)).Length);
            IEnemy newEnemy = null;

            switch (newEnemyKind)
            {
                case Enemies.Rock:
                    newEnemy = new Rock(new Vector(Random.Next(consoleWidth), 0), new Vector(0, 1));
                    break;
                case Enemies.Marine:
                    newEnemy = new Marine(new Vector(Random.Next(consoleWidth), 0), new Vector(0, 1));
                    break;
                case Enemies.Dron:
                    newEnemy = new Dron(new Vector(Random.Next(consoleWidth), 0), new Vector(0, 1));
                    break;
                case Enemies.Droid:
                    newEnemy = new Droid(new Vector(Random.Next(consoleWidth), 0), new Vector(0, 1));
                    break;
                case Enemies.Sergeant:
                    newEnemy = new Sergeant(new Vector(Random.Next(consoleWidth), 0), new Vector(0, 1));
                    break;
                default:
                    break;
            }

            if (newEnemy != null)
            {
                GameObjects.Add(newEnemy);
            }
        }

        private void CleanList<T>(List<T> list, List<int> indices)
        {
            for (int i = indices.Count - 1; i >= 0; i--)
            {
                list.RemoveAt(indices[i]);
            }
        }

        private void PrintWelcomeMessage()
        {

        }

        private void PrintScore()
        {
            string score = "Score: " + this.Player.Score;

            Console.SetCursorPosition(consoleWidth / 2 - score.Length, 0);
            Console.WriteLine(score);
        }
    }
}
