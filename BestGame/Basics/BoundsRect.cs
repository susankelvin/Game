using System;

namespace BestGame
{
    /// <summary>
    /// Rectangular area where drawable object draws itself within.
    /// </summary>
    /// <remarks>
    /// If object draws itself with a single character then both vectors have the same value.
    /// This conception allows engine to know space needed by object to draw itself.
    /// </remarks>
    public struct BoundsRect
    {
        public Vector TopLeft;
        public Vector BottomRight;

        /// <summary>
        /// Width of bounding rectangle.
        /// </summary>
        public int Width
        {
            get
            {
                return BottomRight.X - TopLeft.X + 1;
            }
        }

        /// <summary>
        /// Height of bounding rectangle.
        /// </summary>
        public int Height
        {
            get
            {
                return BottomRight.Y - TopLeft.Y + 1;
            }
        }

        public int Left
        {
            get
            {
                return TopLeft.X;
            }
        }

        public int Top
        {
            get
            {
                return TopLeft.Y;
            }
        }

        public int Right
        {
            get
            {
                return BottomRight.X;
            }
        }

        public int Bottom
        {
            get
            {
                return BottomRight.Y;
            }
        }

        /// <summary>
        /// Creates BoundsRect by top-left and bottom-right edges.
        /// </summary>
        /// <param name="topLeft">Top-left edge.</param>
        /// <param name="bottomRight">Bottom-rigth edge.</param>
        public BoundsRect(Vector topLeft, Vector bottomRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        }

        /// <summary>
        /// Creates BoundsRect by X and Y coordinates of top-left and bottom-right edges.
        /// </summary>
        /// <param name="top">X coordinate of top-left edge.</param>
        /// <param name="left">Y coordinate of top-left edge.</param>
        /// <param name="bottom">X coordinate of bottom-right edge.</param>
        /// <param name="right">Y coordinate of bottom-right edge.</param>
        public BoundsRect(int left, int top, int right, int bottom)
            : this(new Vector(left, top), new Vector(right, bottom))
        { }

        /// <summary>
        /// Checks if bounding rectangle fits into another one.
        /// </summary>
        /// <remarks>
        /// A rectangle fits into outer one if edges of the former are inside later or on its borders.
        /// </remarks>
        /// <param name="outer">The outer bounding rectangle.</param>
        /// <returns>True, if instance lies inside outer rectangle.</returns>
        public bool LiesInside(BoundsRect outer)
        {
            return (this.TopLeft.X >= outer.TopLeft.X) && (this.TopLeft.Y >= outer.TopLeft.Y) &&
                (this.BottomRight.X <= outer.BottomRight.X) && (this.BottomRight.Y <= outer.BottomRight.Y);
        }

        public bool Intersects(BoundsRect other)
        {
            for (int i = this.TopLeft.X; i <= this.BottomRight.X; i++)
            {
                for (int j = this.TopLeft.Y; j <= this.BottomRight.Y; j++)
                {
                    for (int k = other.TopLeft.X; k <= other.BottomRight.X; k++)
                    {
                        for (int l = other.TopLeft.Y; l <= other.BottomRight.Y; l++)
                        {
                            if (new Vector(i, j) == new Vector(k, l))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
