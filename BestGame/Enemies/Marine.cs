using System;

namespace BestGame
{
    public class Marine : DestructableEnemy, IOffencible
    {
        private byte offenceCounter;

        public Marine(Vector position, Vector motionDirection)
            : base(position, motionDirection, 50)
        {
            this.Color = new Colors(ConsoleColor.White, ConsoleColor.Green);
            this.Glyph = "[Ѽ]";
            this.offenceCounter = 0;
        }

        public IWeapon Shoot()
        {
            if (this.offenceCounter++ % 15 == 0)
            {
                return new Bullet(new Vector(this.Position.X, this.Position.Y + 1), new Vector(0, 1));
            }
            else
            {
                return null;
            }
        }
    }
}
