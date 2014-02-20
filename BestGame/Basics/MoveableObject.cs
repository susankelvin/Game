using System;

namespace BestGame
{
    /// <summary>
    /// Abstract base class for every obect that can change its position.
    /// </summary>
    public abstract class MoveableObject : GameObject, IMoveable
    {
        /// <summary>
        /// Motion vector.
        /// </summary>
        protected Vector Direction { get; set; }

        /// <summary>
        /// Creates moveable object with position, colors, text glyph and motion direction.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="motionDirection">Motion direction.</param>
        public MoveableObject(Vector position, Vector motionDirection)
            : base(position)
        {
            this.Direction = motionDirection;
        }

        /// <summary>
        /// Returns the bounding rectangle of object advanced according to motion vector.
        /// </summary>
        /// <returns>Updated bounding rectangle.</returns>
        public virtual BoundsRect Advance()
        {
            return new BoundsRect(this.BoundsRect.TopLeft + this.Direction,
                this.BoundsRect.BottomRight + this.Direction);
        }

        /// <summary>
        /// Moves the object to new position.
        /// </summary>
        /// <param name="newPosition">New position.</param>
        public void Move(Vector newPosition)
        {
            this.Position = newPosition;
        }
    }
}
