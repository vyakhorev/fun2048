using Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using VisualSO;

namespace GameCoreController
{
    /*
     * Helps with setting up new chips
     * TODO - move object pools here and make this thing responsible
     * for all construction / destruction. 
     */
    public class ChipProducer
    {
        private Camera _camera;
        private GameObject _gridCellPrefab;
        private GameObject _numberChipPrefab;
        private SOBoardVisualStyle _soBoardVisualStyle;
        private GameObjectPools _pool;

        private float _cellSize;
        private float _worldSize;
        private float _vertAlgn;
        private float _horAlgn;
        private float _numberChipSpriteSize;
        private float _visualsScale;

        public void Init(
            Camera camera,
            Transform boardParentTransform,
            GameObject numberChipPrefab,
            GameObject gridCellPrefab,
            SOBoardVisualStyle soBoardVisualStyle)
        {
            _camera = camera;
            _soBoardVisualStyle = soBoardVisualStyle;
            _gridCellPrefab = gridCellPrefab;
            _numberChipPrefab = numberChipPrefab;

            _pool = new GameObjectPools(boardParentTransform, 10);
            // Warm-up pools
            _pool.EnsurePoolDefinition(_gridCellPrefab, 16);
            _pool.EnsurePoolDefinition(_numberChipPrefab, 16);

            NumberChipVisuals numVis = _numberChipPrefab.GetComponentInChildren<NumberChipVisuals>();
            SpriteRenderer numSr = numVis.GetComponentInChildren<SpriteRenderer>();

            // Have no idea why I should divide it by 10
            _numberChipSpriteSize = Mathf.Max(
                numSr.sprite.bounds.size.x,
                numSr.sprite.bounds.size.y
            ) / 10f;

            float worldHeight = _camera.orthographicSize * 2f;
            float worldWidth = worldHeight / Screen.height * Screen.width;

            _horAlgn = worldWidth / 2f;
            _vertAlgn = worldHeight / 2f;

            _worldSize = Mathf.Min(
                worldWidth, 
                worldHeight
            );
        }

        public void InitNewGame(Vector2Int boardSize)
        { 
            int maxBoardSize = Mathf.Max(boardSize.x+1, boardSize.y+1);
            _cellSize = _worldSize / maxBoardSize;
            _visualsScale = _cellSize / _numberChipSpriteSize;
        }

        public ChipCtrl SpawnChip(Vector2Int logicalPosition, int val)
        {
            GameObject chipGo = _pool.PoolObject(_numberChipPrefab);
            chipGo.SetActive(true);

            chipGo.transform.SetPositionAndRotation(
                LogicalToWorld(logicalPosition),
                Quaternion.identity
            );
            chipGo.transform.localScale = Vector3.zero;
            ChipCtrl chipCtrl = chipGo.GetComponent<ChipCtrl>();
            ScalableVisuals scVis = chipGo.GetComponentInChildren<ScalableVisuals>();

            scVis.transform.localScale = new Vector3(
                _visualsScale, 
                _visualsScale, 
                1
            );

            UpdateNumberVisuals(chipCtrl, val);

            return chipCtrl;
        }

        public void UpdateNumberVisuals(ChipCtrl chipCtrl, int val)
        {
            SONumberVisualStyle style = _soBoardVisualStyle.NumberVisualStyles[val];
            chipCtrl.SetNumber(val);
            chipCtrl.SetColor(style.ChipColor);
        }

        public Vector3 LogicalToWorld(Vector2Int logicalPosition)
        {
            return new Vector3(
                _cellSize * (logicalPosition.x + 1) - _horAlgn,
                _cellSize * (logicalPosition.y + 1) - _vertAlgn,
                0f
            );

        }


    }
}
