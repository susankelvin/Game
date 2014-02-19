using System;

namespace BestGame
{
    /// <summary>
    /// Required for every object that can change its position on console.
    /// </summary>
    public interface IMoveable : IDrawable
    {
        /// <summary>
        /// Returns the bounding rectangle of new position of the object.
        /// </summary>
        /// <remarks>
        /// The method doesn't change the position of object but gives the engine idea
        /// where the next locatios should be according to the logic of the object.
        /// </remarks>
        /// <returns>New position on console.</returns>
        BoundsRect Advance();

        /// <summary>
        /// Moves the object to new position.
        /// </summary>
        /// <remarks>
        /// The method is called by engine to set the new position of the object.
        /// </remarks>
        /// <param name="newPosition">New position.</param>
        void Move(Vector newPosition);
    }
}
