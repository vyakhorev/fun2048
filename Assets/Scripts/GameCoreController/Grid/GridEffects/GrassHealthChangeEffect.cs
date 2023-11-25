using UnityEngine;

namespace GameCoreController
{
    public class GrassHealthChangeEffect : AGridEffect
    {
        public Vector2Int CellCoords;
        public int HealthLevel;

        public GrassHealthChangeEffect(
            Vector2Int cellCoords, 
            int healthLevel)
        {
            CellCoords = cellCoords;
            HealthLevel = healthLevel;
        }

    }
}
