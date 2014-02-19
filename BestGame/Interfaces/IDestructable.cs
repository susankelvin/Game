using System;

namespace BestGame
{
    /// <summary>
    /// Required for enemy objects that could be destroyed.
    /// </summary>
    public interface IDestructable : IEnemy
    {
        /// <summary>
        /// Shield level.
        /// </summary>
        int Shield { get; }
    }
}
