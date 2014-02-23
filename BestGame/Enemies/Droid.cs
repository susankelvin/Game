using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestGame
{
    class Droid : DestructableEnemy, IOffencible
    {
        private byte offenceCounter;

        public Droid(Vector position, Vector motionDirection)
            : base(position, motionDirection, 100)
        {
            this.Color = new Colors(ConsoleColor.Cyan, ConsoleColor.DarkBlue);
            this.Glyph = "\\/";
            this.offenceCounter = 0;
            this.Shield = 20;
        }

        public IList<IWeapon> Shoot()
        {
            List<IWeapon> bullets = new List<IWeapon>();
            if (this.offenceCounter++ % 30 == 0)
            {
                bullets.Add(new Bullet(new Vector(this.Position.X, this.Position.Y + 1), new Vector(0, 1)));
                return bullets;
            }
            else
            {
                return null;
            }
        }

        public override BoundsRect Advance()
        {
            if (this.offenceCounter % 2 == 0)
            {
                return new BoundsRect(this.Position.X + 1, this.Position.Y + 1);
            }
            else
            {
                return new BoundsRect(this.Position.X - 1, this.Position.Y + 1);
            }

        }

        System.Collections.Generic.IList<IWeapon> IOffencible.Shoot()
        {
            List<IWeapon> bullets = new List<IWeapon>();
            if (this.offenceCounter++ % 30 == 0)
            {
                bullets.Add(new Bullet(new Vector(this.Position.X, this.Position.Y + 1), new Vector(0, 1)));
                return bullets;
            }
            else
            {
                return bullets;
            }
        }
    }
}
