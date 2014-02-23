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

        public IWeapon Shoot()
        {
            if (this.offenceCounter++ % 10 == 0)
            {
                return new Bullet(new Vector(this.Position.X + 1, this.Position.Y), new Vector(0, 1), new Colors(ConsoleColor.White, ConsoleColor.White));
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
    }
}
