using System;

namespace BestGame
{
    public class Marine : DestructableEnemy
    {
        public Marine(Vector position, Vector motionDirection)
            : base(position, motionDirection, 50)
        {
            this.Color = new Colors(ConsoleColor.White, ConsoleColor.Green);
            this.Glyph = "[Ѽ]";
        }
    }
}
