using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestGame
{
    class Rock : Enemy
    {
        public Rock(Vector position, Vector motionDirection)
            : base(position, motionDirection)
        {
            this.Color = new Colors(ConsoleColor.DarkGray, ConsoleColor.DarkGray);
            this.Glyph = "[]";
        }

        public override void Hit(int hitEnergy)
        {
            this.Color = new Colors(ConsoleColor.Red, ConsoleColor.Red);
        }
    }
}
