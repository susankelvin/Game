using System;

namespace BestGame
{
    public interface IStatusBar : IDrawable
    {
        void SetPostion(Vector topLeft);

        void WeaponChangeEventHanlder(Weapons activeWeapon);

        void ScoreUpdateEventHanlder(long newScore);
    }
}
