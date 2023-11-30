using LegacyPooling;
using UnityEngine;
using SO2048;

namespace GameCoreController
{
    /*
     * Helps with setting up new chips
     * for all construction / destruction. 
     */
    public class ChipProducer
    {
        private Camera _camera;
        private GameObject _gridCellPrefab;
        private GameObject _chipPrefab;
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
            SOBoardVisualStyle soBoardVisualStyle)
        {
            _camera = camera;
            _soBoardVisualStyle = soBoardVisualStyle;
            _gridCellPrefab = soBoardVisualStyle.GridCellPrefab;
            _chipPrefab = soBoardVisualStyle.ChipPrefab;

            _pool = new GameObjectPools(boardParentTransform, 10);
            // Warm-up pools
            _pool.EnsurePoolDefinition(_gridCellPrefab, 64);
            _pool.EnsurePoolDefinition(_chipPrefab, 64);

            CmpNumberChipVisuals numVis = _chipPrefab.GetComponentInChildren<CmpNumberChipVisuals>();
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
                _cellSize = _worldSize / (boardSize.y + 2);
            }
            else
            {
                _cellSize = _worldSize / (boardSize.x + 2);
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

        public ChipCtrl SpawnNumberChip(Vector2Int logicalPosition, int val)
        {
            ChipCtrl chipCtrl = SpawnAChip(logicalPosition);
            chipCtrl.SpawnAsNumber();
            UpdateNumberVisuals(chipCtrl, val);
            return chipCtrl;
        }

        public ChipCtrl SpawnStoneChip(Vector2Int logicalPosition, int health)
        {
            ChipCtrl chipCtrl = SpawnAChip(logicalPosition);
            chipCtrl.SpawnAsStone();
            return chipCtrl;
        }

        public ChipCtrl SpawnEggChip(Vector2Int logicalPosition, int health)
        {
            ChipCtrl chipCtrl = SpawnAChip(logicalPosition);
            chipCtrl.SpawnAsEgg();
            return chipCtrl;
        }

        public ChipCtrl SpawnBubbleChip(Vector2Int logicalPosition, int bubbleValue)
        {
            ChipCtrl chipCtrl = SpawnAChip(logicalPosition);
            chipCtrl.SpawnAsBubble();
            return chipCtrl;
        }

        public ChipCtrl SpawnBoosterChip(Vector2Int logicalPosition)
        {
            ChipCtrl chipCtrl = SpawnAChip(logicalPosition);
            chipCtrl.SpawnAsBooster();
            return chipCtrl;
        }

        private ChipCtrl SpawnAChip(Vector2Int logicalPosition)
        {
            GameObject chipGo = _pool.PoolObject(_chipPrefab);
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

            return chipCtrl;
        }


        public void UpdateNumberVisuals(ChipCtrl chipCtrl, int val)
        {
            //SONumberVisualStyle style = _soBoardVisualStyle.GetNumberVisualStyle(val);
            //chipCtrl.SetNumber(val);
            //chipCtrl.SetColor(style.ChipColor);
        }

        public Vector3 LogicalToWorld(Vector2Int logicalPosition)
        {
            return new Vector3(
                _cellSize * (logicalPosition.x + 1) - _horAlgn,
                _cellSize * (logicalPosition.y + 1) - _vertAlgn,
                0f
            );
        }

        public Vector2Int WorldToLogical(Vector2 worldPosition)
        {
            return new Vector2Int(
                Mathf.RoundToInt((worldPosition.x + _horAlgn - _cellSize) / _cellSize),
                Mathf.RoundToInt((worldPosition.y + _vertAlgn - _cellSize) / _cellSize)
            );
        }
    }
}