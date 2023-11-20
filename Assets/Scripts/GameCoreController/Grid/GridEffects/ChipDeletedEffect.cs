using UnityEngine;

namespace GameCoreController
{
    /*
     * This chip is deleted
     */
    public class ChipDeletedEffect : AGridEffect
    {
        public readonly AChip Chip;

        public ChipDeletedEffect(AChip chip)
        {
            Chip = chip;
        }

    }
}
