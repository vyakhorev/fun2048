using UnityEngine;

namespace GameCoreController
{
    public class ChipNumberChangedEffect : AGridEffect
    {
        public NumberChip Chip;

        public ChipNumberChangedEffect(NumberChip chip)
        {
            Chip = chip;
        }

    }
}
