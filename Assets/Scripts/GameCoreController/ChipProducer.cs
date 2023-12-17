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
        //private float _worldSize;
        private float _vertAlgn;
        private float _horAlgn;
        private float _originalCellSize;
        private float _visualsScale;
        private Vector3[] _worldConers;

        public void Init(
            Camera camera,
            Transform boardParentTransform,
            SOBoardVisualStyle soBoardVisualStyle,
            Vector3[] worldCorners)
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

            _worldConers = worldCorners;

            _worldHeight = _worldConers[1].y - _worldConers[0].y;
            _worldWidth = _worldConers[3].x - _worldConers[0].x;

            //_worldSize = Mathf.Min(_worldHeight, _worldWidth);
        }

        public float GetAnimSpeed()
        {
            return _soBoardVisualStyle.AnimSpeed;
        }

        public void InitNewGame(Vector2Int boardSize)
        {
            _calculatedCellSize = Mathf.Min(
                _worldWidth / boardSize.x, 
                _worldHeight / boardSize.y
            );
            _horAlgn = (
                _worldConers[0].x +
                _worldConers[3].x -
                _calculatedCellSize * boardSize.x
            ) / 2f;
            _vertAlgn = (
                _worldConers[1].y +
                _worldConers[0].y -
                _calculatedCellSize * boardSize.y
            ) / 2f;

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
            gridCellCtrl.InitHierarchy();
            CmpScalableVisuals scVis = gridCellGo.GetComponentInChildren<CmpScalableVisuals>();

            scVis.transform.localScale = new Vector3(
                _visualsScale,
                _visualsScale,
                1
            );
            gridCellCtrl.SetEvenBackgroundColor(logicalPosition);

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

        public ChipCtrl SpawnBombChip(Vector2Int logicalPosition)
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
                _calculatedCellSize * (logicalPosition.x) + _horAlgn  + _calculatedCellSize * 0.5f,
                _calculatedCellSize * (logicalPosition.y) + _vertAlgn + _calculatedCellSize  * 0.5f,
                0f
            );
        }

        public Vector2Int WorldToLogical(Vector2 worldPosition)
        {
            return new Vector2Int(
                Mathf.RoundToInt((worldPosition.x - _horAlgn) / _calculatedCellSize - 0.5f),
                Mathf.RoundToInt((worldPosition.y - _vertAlgn) / _calculatedCellSize - 0.5f)
            );
        }
    }
}