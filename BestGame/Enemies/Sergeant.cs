﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestGame
{
    public class Sergeant : DestructableEnemy, IOffencible
    {
        private Random rand = new Random();
        private byte offenceCounter;
        public Sergeant(Vector position, Vector motionDirection)
            : base(position, motionDirection, 100)
        {
            this.Color = new Colors(ConsoleColor.White, ConsoleColor.Red);
            this.Glyph = "\\V/";
            this.offenceCounter = 0;
        }
        public IWeapon Shoot()
        {
            if (this.offenceCounter++ % 30 == 0)
            {
                return new Bullet(new Vector(this.Position.X, this.Position.Y + 1), new Vector(0, 1));
            }
            else
            {
                return null;
            }
        }
        public override BoundsRect Advance()
        {

            Vector vect = new Vector(this.rand.Next(-1, 2), 1);
            var asd = new BoundsRect(this.BoundsRect.TopLeft + vect,
                this.BoundsRect.BottomRight + vect);
            return asd;
        }
    }
}
