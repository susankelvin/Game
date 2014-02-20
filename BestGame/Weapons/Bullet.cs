using System;

namespace BestGame
{
    public class Bullet : Weapon
    {
        public Bullet(Vector position, Vector motionDirection)
            : base(position, motionDirection, 50)
        {
            this.Color = new Colors(ConsoleColor.Red, ConsoleColor.Yellow);
            this.Glyph = "ʘ";
        }
    }
}
