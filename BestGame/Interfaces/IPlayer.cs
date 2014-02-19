using System;

namespace BestGame
{
    /// <summary>
    /// Delegate for IPlayer.WeaponChange event.
    /// </summary>
    public delegate void WeaponChangeEvent();

    /// <summary>
    /// Delegate for IPlayer.ScoreUpdate event.
    /// </summary>
    public delegate void ScoreUpdateEvent();

    /// <summary>
    /// Required for player objects.
    /// </summary>
    public interface IPlayer : IMoveable
    {
        /// <summary>
        /// Active weapon.
        /// </summary>
        Weapons ActiveWeapon { get; }

        /// <summary>
        /// Current score.
        /// </summary>
        long Score { get; set; }

        /// <summary>
        /// Triggered after active weapon was changed.
        /// </summary>
        event WeaponChangeEvent WeaponChange;

        /// <summary>
        /// Triggered after score was updated.
        /// </summary>
        event ScoreUpdateEvent ScoreUpdate;

        /// <summary>
        /// Make weapon available to player.
        /// </summary>
        /// <param name="weapon">Weapon.</param>
        void AddWeapon(Weapons weapon);

        /// <summary>
        /// Change active weapon if available.
        /// </summary>
        /// <param name="index">Weapon.</param>
        /// <returns>True, if weapon is activated.</returns>
        bool SelectWeapon(int index);

        /// <summary>
        /// Shoots with active weapon.
        /// </summary>
        /// <returns>New shot.</returns>
        Weapon Shoot();
    }
}
