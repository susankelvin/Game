using System;

namespace BestGame
{
    /// <summary>
    /// Required for every object visible on console.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Bounding rectangle of object.
        /// </summary>
        BoundsRect BoundsRect { get; }

        /// <summary>
        /// Draws itself onto console.
        /// </summary>
        void Draw();
    }
}
