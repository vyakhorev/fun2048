using UnityEngine;

namespace Fun2048
{
    public class ChipMoveEffect : AGridEffect
    {
        public readonly AChip Chip;
        public readonly Vector2Int PointTo;

        public ChipMoveEffect(AChip chip, Vector2Int pointTo)
        {
            Chip = chip;
            PointTo = pointTo;
        }

    }
}
