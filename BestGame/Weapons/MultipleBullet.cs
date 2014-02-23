using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestGame
{
    public class MultipleBullet : Weapon
    {
        public MultipleBullet(Vector position, Vector motionDirection)
            : base(position, motionDirection, 50)
        {
            this.Color = new Colors(ConsoleColor.Red, ConsoleColor.Yellow);
            this.Glyph = "ʘ";
        }
    }
}
