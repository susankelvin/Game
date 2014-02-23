using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestGame
{
    class Dron : DestructableEnemy, IOffencible
    {
        private byte offenceCounter;

        public Dron(Vector position, Vector motionDirection)
            : base(position, motionDirection, 50)
        {
            this.Color = new Colors(ConsoleColor.Red, ConsoleColor.DarkBlue);
            this.Glyph = "\\__/";
            this.offenceCounter = 0;
            this.Shield = 150;
        }

        public IWeapon Shoot()
        {
            if (this.offenceCounter++ % 3 == 0)
            {
                return new Bullet(new Vector(this.Position.X + 1, this.Position.Y + 1), new Vector(0, 1), new Colors(ConsoleColor.Red, ConsoleColor.Red));
            }
            else
            {
                return null;
            }
        }
    }
}
