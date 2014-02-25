using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASCIIInvaders
{
    public class Knife : Weapon
    {
        private const int LifeDurationInMoves = 20;
        private int movesCount;

        public Knife(Vector position, Vector motionDirection)
            : base(position, motionDirection, 25)
        {
            this.Color = new Colors(ConsoleColor.Magenta, ConsoleColor.Black);
            this.Glyph = "ǂ";
            this.movesCount = 0;
        }

        public override BoundsRect Advance()
        {
            if (++this.movesCount == LifeDurationInMoves)
            {
                this.Energy = 0;
            }

            return base.Advance();
        }
    }
}
