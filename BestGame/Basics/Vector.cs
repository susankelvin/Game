using System;

namespace BestGame
{
    /// <summary>
    /// Coordinates of top-left point of drawable object or motion direction.
    /// </summary>
    public struct Vector 
    {
        public int X;
        public int Y;

        /// <summary>
        /// Creates new Vector.
        /// </summary>
        /// <param name="x">X position or motion direction.</param>
        /// <param name="y">Y position or motion direction.</param>
        public Vector(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static bool operator ==(Vector a, Vector b)
        {
            return (a.X == b.X) && (a.Y == b.Y);
        }
             
        public static bool operator !=(Vector a, Vector b)
        {
            return (a.X != b.X) || (a.Y != b.Y);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return this == (Vector)obj;
        }

        public override int GetHashCode()
        {
            return this.X | (this.Y << 16);
        }
    }
}
