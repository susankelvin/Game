using System;
using System.Collections.Generic;

namespace BestGame
{
    /// <summary>
    /// Required for objects that can attack.
    /// </summary>
    public interface IOffencible
    {
        /// <summary>
        /// Called by engine when object can attack with any of its weapons.
        /// </summary>
        /// <remarks>
        /// It is not required object to attack every time method is called.
        /// Return null if object will not attack this time.
        /// </remarks>
        /// <returns>New shot or null.</returns>
        IList<IWeapon> Shoot();
    }
}
