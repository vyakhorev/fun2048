using UnityEngine;

namespace Fun2048
{
    public class ChipsMergeEffect : AGridEffect
    {
        public readonly NumberChip ChipFrom;
        public readonly NumberChip ChipTo;
        public readonly Vector2Int PointFrom;
        public readonly Vector2Int PointTo;

        public ChipsMergeEffect(NumberChip chipFrom, NumberChip chipTo, Vector2Int pointFrom, Vector2Int pointTo)
        {
            ChipFrom = chipFrom;
            ChipTo = chipTo;
            PointFrom = pointFrom;
            PointTo = pointTo;
        }

    }
}
