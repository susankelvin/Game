using System;
using System.Collections.Generic;

namespace BestGame
{
    public class Player : MoveableObject, IPlayer, IOffencible
    {
        private long score;

        protected bool[] AvailableWeapons { get; set; }

        protected int CurrentWeapon { get; set; }

        public Weapons ActiveWeapon
        {
            get
            {
                return (Weapons)CurrentWeapon;
            }
        }

        public long Score
        {
            get
            {
                return this.score;
            }

            set
            {
                this.score = value;

                if (ScoreUpdate != null)
                {
                    ScoreUpdate(this.Score);
                }
            }
        }

        public event WeaponChangeEvent WeaponChange;

        public event ScoreUpdateEvent ScoreUpdate;

        public Player(Vector position)
            : base(position, new Vector(0, 0))
        {
            this.AvailableWeapons = new bool[Enum.GetValues(typeof(Weapons)).Length];
            this.Score = 0;
            this.Glyph = "(M)";
            this.Color = new Colors(ConsoleColor.White, ConsoleColor.DarkRed);
        }

        public void AddWeapon(Weapons weapon)
        {
            this.AvailableWeapons[(int)weapon] = true;
        }

        public bool SelectWeapon(int index)
        {
            if ((index < 0) || (index >= Enum.GetValues(typeof(Weapons)).Length))
            {
                return false;
            }

            if (this.AvailableWeapons[index])
            {
                CurrentWeapon = index;

                if (WeaponChange != null)
                {
                    WeaponChange(this.ActiveWeapon);
                }
            }

            return this.AvailableWeapons[index];
        }

        public IList<IWeapon> Shoot()
        {
            List<IWeapon> result = new List<IWeapon>();

            switch (ActiveWeapon)
            {
                case Weapons.Knife:
                    return null;
                case Weapons.Bullet:
                    result.Add(new Bullet(new Vector(this.Position.X, this.Position.Y - 1),
                        new Vector(0, -1)));
                    return result;
                case Weapons.Missile:
                    result.Add(new Missile(new Vector(this.Position.X, this.Position.Y - 1), new Vector(0, -1)));
                    return result;
                case Weapons.MultipleBullet:
                    result.Add(new MultipleBullet(new Vector(this.Position.X - 1, this.Position.Y - 1), new Vector(0, -1)));
                    result.Add(new MultipleBullet(new Vector(this.Position.X, this.Position.Y - 1), new Vector(0, -1)));
                    result.Add(new MultipleBullet(new Vector(this.Position.X + 1, this.Position.Y - 1), new Vector(0, -1)));
                    return result;
                default:
                    return null;
            }
        }
    }
}
