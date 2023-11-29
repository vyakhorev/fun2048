using UnityEngine;

namespace GameCoreController
{
    public class ChipSpawnedEffect : AGridEffect
    {
        public AChip SpawnedChip;
        public Vector2Int Coords;

        public ChipSpawnedEffect(AChip chip, Vector2Int coords)
        {
            SpawnedChip = chip;
            Coords = coords;
        }

    }
}
