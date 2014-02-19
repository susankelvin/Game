using System;

namespace BestGame
{
    /// <summary>
    /// Fundamental type supported by game engine.
    /// </summary>
    public abstract class GameObject : IDrawable
    {
        /// <summary>
        /// Position of top left corner of the glyph.
        /// </summary>
        public Vector Position { get; protected set; }

        /// <summary>
        /// Colors of the object.
        /// </summary>
        protected Colors Color { get; set; }

        /// <summary>
        /// String representation of object that is drawn onto console.
        /// </summary>
        protected string Glyph { get; set; }

        /// <summary>
        /// Bounding rectangle of object.
        /// </summary>
        /// <remarks>
        /// Must be overriden by objects with glyphs on two or more rows.
        /// </remarks>
        public virtual BoundsRect BoundsRect
        {
            get
            {
                return new BoundsRect(this.Position.X, this.Position.Y,
                    this.Position.X + this.Glyph.Length - 1, this.Position.Y);
            }

        }

        /// <summary>
        /// Creates new drawable object with position, colors and text glyph.
        /// </summary>
        /// <param name="position">Position of top left corner.</param>
        /// <param name="color">Object's colors.</param>
        public GameObject(Vector position)
        {
            this.Position = position;
            this.Color = new Colors(ConsoleColor.White, ConsoleColor.Black);
            this.Glyph = null;
        }

        /// <summary>
        /// Draws object onto console.
        /// </summary>
        /// <remarks>
        /// Must be overriden by objects with glyphs on two or more rows.
        /// </remarks>
        public virtual void Draw()
        {
            Console.ForegroundColor = this.Color.Foreground;
            Console.BackgroundColor = this.Color.Background;
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(this.Glyph);
        }
    }
}
