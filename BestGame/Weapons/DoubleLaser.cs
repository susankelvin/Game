using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASCIIInvaders
{
    public class DoubleLaser : Weapon
    {
        public DoubleLaser(Vector position, Vector motionDirection)
            : base(position, motionDirection, 500)
        {
            this.Color = new Colors(ConsoleColor.DarkMagenta, ConsoleColor.Cyan);
            this.Glyph = "|";
        }
    }
}
