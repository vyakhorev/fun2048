using UnityEngine;

namespace GameCoreController
{
    /*
     * Explicit info about 2 chips being merged
     */
    public class ChipsMergeEffect : AGridEffect
    {
        public readonly NumberChip ChipFrom;
        public readonly NumberChip ChipTo;

        public ChipsMergeEffect(NumberChip chipFrom, NumberChip chipTo)
        {
            ChipFrom = chipFrom;
            ChipTo = chipTo;
        }

    }
}
