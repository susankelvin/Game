using System;

namespace ASCIIInvaders
{
    public interface IWeapon : IMoveable
    {
        int Energy { get; }

        int Attack(IDestructable target);
    }
}
