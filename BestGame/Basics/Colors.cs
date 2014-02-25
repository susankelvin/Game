using System;

namespace ASCIIInvaders
{
    /// <summary>
    /// Foreground and background colors of drawable object.
    /// </summary>
    public struct Colors
    {
        public ConsoleColor Foreground;
        public ConsoleColor Background;

        public Colors(ConsoleColor fore, ConsoleColor back)
        {
            this.Foreground = fore;
            this.Background = back;
        }
    }
}
