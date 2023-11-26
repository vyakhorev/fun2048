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
        private float _worldWidth;
        private float _worldHeight;
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
            _pool.EnsurePoolDefinition(_gridCellPrefab, 64);
            _pool.EnsurePoolDefinition(_numberChipPrefab, 64);

            CmpNumberChipVisuals numVis = _numberChipPrefab.GetComponentInChildren<CmpNumberChipVisuals>();
            SpriteRenderer numSr = numVis.GetComponentInChildren<SpriteRenderer>();

            float imageScale = Mathf.Min(
                numSr.transform.localScale.x, 
                numSr.transform.localScale.y
            );
            _numberChipSpriteSize = Mathf.Max(
                numSr.sprite.bounds.size.x,
                numSr.sprite.bounds.size.y
            ) * imageScale;

            _worldHeight = _camera.orthographicSize * 2f;
            _worldWidth = _worldHeight / Screen.height * Screen.width;
            _worldSize = Mathf.Min(_worldHeight, _worldWidth);

            _horAlgn = _worldWidth / 2f;
            _vertAlgn = _worldHeight / 2f;

        }

        public void InitNewGame(Vector2Int boardSize)
        {

            if (boardSize.y <= boardSize.x)
            {
                _cellSize = _worldSize / (boardSize.y + 1);
            }
            else
            {
                _cellSize = _worldSize / (boardSize.x + 1);
            }
            _visualsScale = _cellSize / _numberChipSpriteSize;
        }

        public GridCellCtrl SpawnGridCell(Vector2Int logicalPosition)
        {
            GameObject gridCellGo = _pool.PoolObject(_gridCellPrefab);
            gridCellGo.SetActive(true);

            gridCellGo.transform.SetPositionAndRotation(
                LogicalToWorld(logicalPosition),
                Quaternion.identity
            );
            gridCellGo.transform.localScale = Vector3.one;
            GridCellCtrl gridCellCtrl = gridCellGo.GetComponent<GridCellCtrl>();
            CmpScalableVisuals scVis = gridCellGo.GetComponentInChildren<CmpScalableVisuals>();

            scVis.transform.localScale = new Vector3(
                _visualsScale,
                _visualsScale,
                1
            );

            return gridCellCtrl;
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
            CmpScalableVisuals scVis = chipGo.GetComponentInChildren<CmpScalableVisuals>();

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
