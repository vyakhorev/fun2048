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
        public SOBoardVisualStyle SOBoardVisualStyle => _soBoardVisualStyle;
        private GameObjectPools _pool;

        private float _calculatedCellSize;
        private float _worldWidth;
        private float _worldHeight;
        private float _worldSize;
        private float _vertAlgn;
        private float _horAlgn;
        private float _originalCellSize;
        private float _visualsScale;
        private Rect _bounds;

        public void Init(
            Camera camera,
            Transform boardParentTransform,
            SOBoardVisualStyle soBoardVisualStyle,
            Rect bounds)
        {
            _camera = camera;
            _soBoardVisualStyle = soBoardVisualStyle;
            _gridCellPrefab = soBoardVisualStyle.GridCellPrefab;
            _chipPrefab = soBoardVisualStyle.ChipPrefab;

            _pool = new GameObjectPools(boardParentTransform, 10);
            // Warm-up pools
            _pool.EnsurePoolDefinition(_gridCellPrefab, 99);
            _pool.EnsurePoolDefinition(_chipPrefab, 64);

            // Cell can be of different scale, let's use it for scaling
            CmpBackgroundCellVisuals cellVis = _gridCellPrefab.GetComponentInChildren<CmpBackgroundCellVisuals>();
            SpriteRenderer cellSr = cellVis.GetComponentInChildren<SpriteRenderer>(true);
            
            float imageScale = Mathf.Min(
                cellSr.transform.localScale.x,
                cellSr.transform.localScale.y
            );
            _originalCellSize = Mathf.Max(
                cellSr.sprite.bounds.size.x,
                cellSr.sprite.bounds.size.y
            ) * imageScale;

            Debug.Log(_originalCellSize);

            _worldHeight = _camera.orthographicSize * 2f;
            _worldWidth = _worldHeight / Screen.height * Screen.width;
            _worldSize = Mathf.Min(_worldHeight, _worldWidth);
            _horAlgn = _worldWidth / 2f;
            _vertAlgn = _worldHeight / 2f;

            _bounds = bounds;

            Debug.Log(_bounds);

        }

        public float GetAnimSpeed()
        {
            return _soBoardVisualStyle.AnimSpeed;
        }

        public void InitNewGame(Vector2Int boardSize)
        {

            if (boardSize.y <= boardSize.x)
            {
                _calculatedCellSize = _worldSize / (boardSize.y + 1);
            }
            else
            {
                _calculatedCellSize = _worldSize / (boardSize.x + 1);
            }
            _visualsScale = _calculatedCellSize / _originalCellSize;
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
            chipCtrl.SetNumber();
            UpdateNumberVisuals(chipCtrl, val);
            return chipCtrl;
        }

        public ChipCtrl SpawnBoxChip(Vector2Int logicalPosition, int health)
        {
            ChipCtrl chipCtrl = SpawnAChip(logicalPosition);
            chipCtrl.SetBox(health);
            return chipCtrl;
        }

        public ChipCtrl SpawnEggChip(Vector2Int logicalPosition, int health)
        {
            ChipCtrl chipCtrl = SpawnAChip(logicalPosition);
            chipCtrl.SetEgg(health);
            return chipCtrl;
        }

        public ChipCtrl SpawnBubbleChip(Vector2Int logicalPosition, int bubbleValue)
        {
            ChipCtrl chipCtrl = SpawnAChip(logicalPosition);
            chipCtrl.SetBubble();
            return chipCtrl;
        }

        public ChipCtrl SpawnBoosterChip(Vector2Int logicalPosition)
        {
            ChipCtrl chipCtrl = SpawnAChip(logicalPosition);
            chipCtrl.SetBomb();
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
            chipCtrl.SetNumber(val);
        }

        public Vector3 LogicalToWorld(Vector2Int logicalPosition)
        {
            return new Vector3(
                _calculatedCellSize * (logicalPosition.x + 1) - _horAlgn,
                _calculatedCellSize * (logicalPosition.y + 1) - _vertAlgn,
                0f
            );
        }

        public Vector2Int WorldToLogical(Vector2 worldPosition)
        {
            return new Vector2Int(
                Mathf.RoundToInt((worldPosition.x + _horAlgn - _calculatedCellSize) / _calculatedCellSize),
                Mathf.RoundToInt((worldPosition.y + _vertAlgn - _calculatedCellSize) / _calculatedCellSize)
            );
        }
    }
}