using UnityEngine;

namespace GameCoreController
{
    /*
     * This chip is deleted
     */
    public class ChipHealthChangeEffect : AGridEffect
    {
        public readonly AChip Chip;
        public readonly int Health;

        public ChipHealthChangeEffect(AChip chip, int health)
        {
            Chip = chip;
            Health = health;
        }

    }
}
