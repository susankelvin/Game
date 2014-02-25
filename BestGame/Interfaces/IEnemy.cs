using System;

namespace ASCIIInvaders
{
    /// <summary>
    /// Required for enemy objects.
    /// </summary>
    public interface IEnemy : IMoveable
    {
        void Hit(int hitEnergy);
    }
}
