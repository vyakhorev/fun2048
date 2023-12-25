using UnityEngine;

namespace GameCoreController
{
    public class HoneyHealthChangeEffect : AGridEffect
    {
        public Vector2Int CellCoords;
        public int HealthLevel;
        public bool JustSpawned;

        public HoneyHealthChangeEffect(
            Vector2Int cellCoords,
            int healthLevel,
            bool justSpawned)
        {
            CellCoords = cellCoords;
            HealthLevel = healthLevel;
            JustSpawned = justSpawned;
        }

    }
}
