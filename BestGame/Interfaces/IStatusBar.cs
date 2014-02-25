using System;

namespace ASCIIInvaders
{
    public interface IStatusBar : IDrawable
    {
        void SetPostion(Vector topLeft);

        void WeaponChangeEventHanlder(Weapons activeWeapon);

        void ScoreUpdateEventHanlder(long newScore);
    }
}
