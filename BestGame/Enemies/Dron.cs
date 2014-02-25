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
            : base(position, motionDirection, 300)
        {
            this.Color = new Colors(ConsoleColor.Red, ConsoleColor.DarkBlue);
            this.Glyph = "\\__/";
            this.offenceCounter = 0;
        }

        System.Collections.Generic.IList<IWeapon> IOffencible.Shoot()
        {
            List<IWeapon> bullets = new List<IWeapon>();
            if (this.offenceCounter++ % 6 == 0)
            {
                bullets.Add(new Bullet(new Vector(this.Position.X + 2, this.Position.Y + 1), new Vector(0, 1), new Colors(ConsoleColor.Red, ConsoleColor.Red)));
                return bullets;
            }
            else
            {
                return bullets;
            }
        }
    }
}
