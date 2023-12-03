using UnityEngine;

namespace GameCoreController
{
    public class TurnsLeftChangedEffect : AGridEffect
    {
        public int TurnsLeft;

        public TurnsLeftChangedEffect(int turnsLeft)
        {
            TurnsLeft = turnsLeft;
        }

    }
}
