using UnityEngine;

namespace GameCoreController
{
    /*
     * This chip is deleted
     */
    public class BombActivationEffect : AGridEffect
    {
        public readonly BombChip Chip;

        public BombActivationEffect(BombChip chip)
        {
            Chip = chip;
        }

    }
}
