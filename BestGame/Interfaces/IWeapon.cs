using System;

namespace BestGame
{
    public interface IWeapon : IMoveable
    {
        int Energy { get; }

        int Attack(IDestructable target);
    }
}
