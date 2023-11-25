using UnityEngine;

namespace GameCoreController
{
    public class HoneyHealthChangeEffect : AGridEffect
    {
        public Vector2Int CellCoords;
        public int HealthLevel;

        public HoneyHealthChangeEffect(
            Vector2Int cellCoords,
            int healthLevel)
        {
            CellCoords = cellCoords;
            HealthLevel = healthLevel;
        }

    }
}
