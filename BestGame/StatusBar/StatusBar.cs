using System;

namespace BestGame
{
    /// <summary>
    /// Status bar of the game.
    /// </summary>
    /// <remarks>
    /// Status bar occupies the last row(s) of console. Implementation must return the correct
    /// value for BoundsRect.Height in order engine to properly position it.
    /// The engine will subscribe the status bar object to Player's events and this way
    /// status bar will be able to display the most recent information about player's state.
    /// </remarks>
    public class StatusBar : GameObject
    {
        /// <summary>
        /// Delegate for player's WeaponChangeEvent.
        /// </summary>
        public WeaponChangeEvent WeaponChange { get; private set; }

        /// <summary>
        /// Delegate for player's ScoreUpdateEvent.
        /// </summary>
        public ScoreUpdateEvent ScoreUpdate { get; private set; }

        public StatusBar() : base(new Vector(0, 0))
        { 
            // TODO: Assign WeaponChange & ScoreUpdate.

            // Demo code:
            this.Glyph = "Status bar";
            this.Color = new Colors(ConsoleColor.DarkGray, ConsoleColor.White);
        }

        /// <summary>
        /// Called by engine to set position.
        /// </summary>
        /// <param name="topLeft">Top-left edge coordinates.</param>
        public void SetPostion(Vector topLeft)
        {
            this.Position = topLeft;
        }
    }
}
