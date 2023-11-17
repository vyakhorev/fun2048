using UnityEngine;

namespace Fun2048
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
