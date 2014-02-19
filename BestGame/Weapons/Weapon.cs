using System;

namespace BestGame
{
    public class WeaponException : ApplicationException
    {
        public WeaponException(string message)
            : base(message)
        { }
    }

    /// <summary>
    /// Weapons available.
    /// </summary>
    public enum Weapons { Knife, Bullet, Missile, Bomb };

    /// <summary>
    /// Abstract foundation of weapon objects.
    /// </summary>
    public abstract class Weapon : MoveableObject, IWeapon
    {
        /// <summary>
        /// Offence power.
        /// </summary>
        public int Energy { get; protected set; }

        public Weapon(Vector position, Vector motionDirection, int energy)
            : base(position, motionDirection)
        {
            if (energy < 0)
            {
                throw new ArgumentOutOfRangeException("energy");
            }

            this.Energy = energy;
        }

        /// <summary>
        /// Attacks an enemy.
        /// </summary>
        /// <param name="target">Enemy.</param>
        /// <returns>Power of enemy neutralised.</returns>
        public int Attack(IDestructable target)
        {
            int result;
            int oldEnergy;

            if (this.Energy == 0)
            {
                throw new WeaponException("Attempt to attack with no energy");
            }

            if (target == null)
            {
                return 0;
            }

            result = Math.Min(this.Energy, target.Shield);
            oldEnergy = this.Energy;
            this.Energy -= target.Shield;
            target.Hit(oldEnergy);
            return result;
        }
    }
}
