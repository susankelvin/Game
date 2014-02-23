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
