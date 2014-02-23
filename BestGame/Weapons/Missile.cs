using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestGame
{
    public class Missile : Weapon
    {
        public Missile(Vector position, Vector motionDirection)
            : base(position, motionDirection, 15000)
        {
            this.Color = new Colors(ConsoleColor.Red, ConsoleColor.Green);
            this.Glyph = "@";
        }

    }
}
