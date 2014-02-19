using System;

namespace BestGame
{
    /// <summary>
    /// Abstract foundation of destructable enemy objects.
    /// </summary>
    public abstract class DestructableEnemy : Enemy, IDestructable
    {
        /// <summary>
        /// Shield level.
        /// </summary>
        /// <remarks>
        /// When shield level becomes less or equal to zero engine removes the object i.e. it's destroyed.
        /// </remarks>
        public int Shield { get; protected set; }

        public DestructableEnemy(Vector position, Vector motionDirection, int shield)
            : base(position, motionDirection)
        {
            if (shield < 0)
            {
                throw new ArgumentNullException("shield");
            }

            this.Shield = shield;
        }

        public override void Hit(int hitEnergy)
        {
            this.Shield -= hitEnergy;
        }
    }
}
