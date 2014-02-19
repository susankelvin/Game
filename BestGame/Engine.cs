using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace BestGame
{
    public class Engine
    {
        private const int nativeTimeOut = 50;
        private const int updateEnemiesPasses = 5;
        private const int creationPasses = 8;
        private const int arenaWidth = 80;
        private const int arenaHeight = 50;

        private ulong Counter { get; set; }
        private Random Random { get; set; }
        private List<IWeapon> PlayerShots { get; set; }
        private List<IWeapon> EnemyShots { get; set; }
        private IPlayer Player { get; set; }
        private List<IMoveable> GameObjects { get; set; }
        private BoundsRect BattleArena { get; set; }
        private StatusBar StatusBar { get; set; }
        private bool PlayerKilled { get; set; }

        public Engine()
        {
            this.PlayerShots = new List<IWeapon>();
            this.EnemyShots = new List<IWeapon>();
            this.Player = new Player(new Vector(arenaWidth / 2, arenaHeight - 1));
            this.Player.AddWeapon(Weapons.Bullet);
            this.Player.SelectWeapon(1);
            this.GameObjects = new List<IMoveable>();
            this.Counter = 0;
            this.Random = new Random();
            this.BattleArena = new BoundsRect(0, 0, arenaWidth - 1, arenaHeight - 1);
            this.PlayerKilled = false;
        }

        private void InitConsole()
        {
            Console.BufferWidth = arenaWidth + 1;
            Console.WindowWidth = arenaWidth + 1;
            Console.BufferHeight = arenaHeight;
            Console.WindowHeight = arenaHeight;
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

            do
            {
                this.DrawBackground();
                this.Player.Draw();
                this.DrawShots();
                this.DrawObjects();

                this.Counter++;
                Thread.Sleep(nativeTimeOut);

                if (!ReadKeyboard())
                {
                    break;
                }

                if (this.Counter % creationPasses == 0)
                {
                    AddEnemy();
                }

                if (this.Counter % updateEnemiesPasses == 0)
                {
                    this.MoveObjects();
                }

                this.MoveShots();
            } while (!this.PlayerKilled);

            Console.ResetColor();
            Console.Clear();
            this.PrintScore();
        }

        private bool TryDestroy(IMoveable item)
        {
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
                            HandleShotHit(this.PlayerShots[i], this.GameObjects[j]);
                            TryDestroy(this.GameObjects[j]);
                            shotsToRemove.Add(i);
                            shotDestroyed = true;
                            break;
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

        private void ProcessKey(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.Spacebar:
                    PlayerShots.Add(Player.Shoot());
                    break;
                case ConsoleKey.LeftArrow:
                    if (this.Player.BoundsRect.Left > 0)
                    {
                        this.Player.Move(new Vector(this.Player.BoundsRect.Left - 1,
                            this.Player.BoundsRect.Top));
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (this.Player.BoundsRect.Left + this.Player.BoundsRect.Width < arenaWidth)
                    {
                        this.Player.Move(new Vector(this.Player.BoundsRect.Left + 1,
                            this.Player.BoundsRect.Top));
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
                    break;
                case Enemies.Marine:
                    newEnemy = new Marine(new Vector(Random.Next(arenaWidth), 0), new Vector(0, 1));
                    break;
                case Enemies.Dron:
                    break;
                case Enemies.Droid:
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

        private void PrintScore()
        {
            Console.SetCursorPosition(arenaWidth / 2, 0);
            Console.WriteLine("Score: " + this.Player.Score);
        }
    }
}
