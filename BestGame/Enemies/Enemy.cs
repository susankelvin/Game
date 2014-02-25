using System;

namespace ASCIIInvaders
{
    /// <summary>
    /// Available enemies.
    /// </summary>
    public enum Enemies { Rock, Marine, Dron, Droid, Sergeant };

    /// <summary>
    /// Abstract foundation of enemy objects.
    /// </summary>
    public abstract class Enemy : MoveableObject, IEnemy
    {
        public Enemy(Vector position, Vector motionDirection)
            : base(position, motionDirection)
        { }

        /// <summary>
        /// Called when object is under fire.
        /// </summary>
        /// <param name="hitEnergy">The power of attacking weapon.</param>
        public abstract void Hit(int hitEnergy);
    }
}
