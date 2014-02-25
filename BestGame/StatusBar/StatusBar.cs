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
    public class StatusBar : GameObject, IStatusBar
    {
        private Weapons currentWeapon;
        private long currentScore;

        public override BoundsRect BoundsRect
        {
            get
            {
                return new BoundsRect(this.Position.X, this.Position.Y,
                    Console.WindowWidth - this.Position.X - 1, this.Position.Y + 1);
            }
        }

        public StatusBar() : base(new Vector(0, 0))
        {
            this.currentWeapon = (Weapons)0;
            this.currentScore = 0;
            this.Color = new Colors(ConsoleColor.White, ConsoleColor.DarkBlue);
        }

        /// <summary>
        /// Hanlder for player's WeaponChangeEvent.
        public void WeaponChangeEventHanlder(Weapons activeWeapon)
        {
            this.currentWeapon = activeWeapon;
        }

        /// <summary>
        /// Hanlder for player's ScoreUpdateEvent.
        /// </summary>
        public void ScoreUpdateEventHanlder(long newScore)
        {
            this.currentScore = newScore;
        }

        /// <summary>
        /// Called by engine to set position.
        /// </summary>
        /// <param name="topLeft">Top-left edge coordinates.</param>
        public void SetPostion(Vector topLeft)
        {
            this.Position = topLeft;
        }

        public override void Draw()
        {
            string line = new string('\u00AF', this.BoundsRect.Width);

            Console.ForegroundColor = this.Color.Foreground;
            Console.BackgroundColor = this.Color.Background;
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(line);
            Console.SetCursorPosition(this.Position.X, this.Position.Y + 1);
            Console.Write("\tScore: {0}\t\tActive weapon: {1}", this.currentScore, this.currentWeapon);
        }
    }
}
