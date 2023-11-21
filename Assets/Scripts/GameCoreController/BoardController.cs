// using Mocked2048Game;
using Pooling;
using System.Collections.Generic;
using UnityEngine;
using VisualSO;

/*
 * Responsible for board visualisation
 */
namespace GameCoreController
{
    public class BoardController : MonoBehaviour
    {
        private Chip2048Game _chip2048Game;
        private Transform _boardParentTransform;
        private List<GridDirection> _enquedSwipes;
        private bool _readyToPlay = false;
        private GameObjectPools _pool;
        private ChipProducer _chipProducer;

        private Dictionary<int, GridCellCtrl> _gridCellViews;
        private Dictionary<int, ChipCtrl> _numberViews;
        
        private GameObject _gridCellPrefab;
        private GameObject _numberChipPrefab;
        
        private Camera _camera;
        private float _cellSize;
        private float _vertAlgn;
        private float _horAlgn;

        public void Init(
            Transform boardParentTransform,
            GameObject gridCellPrefab,
            GameObject numberChipPrefab,
            SOBoardVisualStyle soBoardVisualStyle)
        {
            _boardParentTransform = boardParentTransform;
            _enquedSwipes = new List<GridDirection>();
            _readyToPlay = false;
            _gridCellPrefab = gridCellPrefab;
            _numberChipPrefab = numberChipPrefab;

            _pool = new GameObjectPools(_boardParentTransform, 10);
            // Warm-up pools
            _pool.EnsurePoolDefinition(_gridCellPrefab, 16);
            _pool.EnsurePoolDefinition(_numberChipPrefab, 16);

            _chipProducer = new ChipProducer();
            _chipProducer.Init(soBoardVisualStyle);

            _camera = Camera.main;
        }

        public void StartNewGame()
        {
            _chip2048Game = new Chip2048Game(4, 4);
            _chip2048Game.ResetGame();
            _numberViews = new Dictionary<int, ChipCtrl>();
            _gridCellViews = new Dictionary<int, GridCellCtrl>();
            _readyToPlay = true;

            Vector2Int boardSize = _chip2048Game.GetBoardSize();
            float worldHeight = _camera.orthographicSize * 2f;
            float worldWidth = worldHeight * _camera.aspect;
            _horAlgn = worldWidth / 2f;
            _vertAlgn = worldHeight / 2f;

            _cellSize = Mathf.Min(
                worldWidth * 0.8f / boardSize.x,
                worldHeight * 0.8f / boardSize.y
            );

        }

        public void ExecuteSwipe(GridDirection gridDirection)
        {
            _enquedSwipes.Add(gridDirection);
        }

        public void Update()
        {
            if (!_readyToPlay) return;

            // Ensure we collect effects after we apply input,
            // release _enquedSwipes asap not to miss user input
            // (and not to get stuck in case of exception)
            List<GridDirection> acts = new List<GridDirection>(_enquedSwipes);
            _enquedSwipes.Clear();
            foreach (GridDirection gridDirection in acts)
            {
                _chip2048Game.TrySwipe(gridDirection);
            }              

            foreach (AGridEffect effect_i in _chip2048Game.GetEffects())
            {
                if (effect_i is BoardResetEffect boardResetEffect) ShowEffect(boardResetEffect);
                else if (effect_i is ChipSpawnedEffect chipSpawnedEffect) ShowEffect(chipSpawnedEffect);
                else if (effect_i is ChipDeletedEffect chipDeletedEffect) ShowEffect(chipDeletedEffect);
                else if (effect_i is ChipMoveEffect chipMoveEffect) ShowEffect(chipMoveEffect);
                else if (effect_i is ChipNumberChangedEffect chipNumberChangedEffect) ShowEffect(chipNumberChangedEffect);
                else if (effect_i is ChipsMergeEffect chipsMergeEffect) ShowEffect(chipsMergeEffect);
            }
            _chip2048Game.ResetEffects();
        }

        private void ShowEffect(BoardResetEffect boardResetEffect)
        {
            Debug.Log("Board reset");
            foreach (KeyValuePair<int, ChipCtrl> entry in _numberViews)
            {
                entry.Value.gameObject.SetActive(false);  // Return to the pool
            }
            _numberViews.Clear();
        }

        private void ShowEffect(ChipSpawnedEffect chipSpawnedEffect)
        {
            int chId = chipSpawnedEffect.SpawnedChip.GetChipId();
            Debug.Log("Spawning " + chId + " at " + chipSpawnedEffect.Coords);

            GameObject numberChipPrefab = _numberChipPrefab;
            GameObject chipGo = _pool.PoolObject(numberChipPrefab);
            chipGo.SetActive(true);

            chipGo.transform.SetPositionAndRotation(
                LogicalToWorld(chipSpawnedEffect.Coords), 
                Quaternion.identity
            );
            ChipCtrl chipCtrl = chipGo.GetComponent<ChipCtrl>();
            _numberViews[chId] = chipCtrl;
            if (chipSpawnedEffect.SpawnedChip is NumberChip numberChip)
            {
                _chipProducer.UpdateNumberVisuals(chipCtrl, numberChip.GetNumericValue());
            }   
        }

        private void ShowEffect(ChipDeletedEffect chipDeletedEffect)
        {
            int chId = chipDeletedEffect.Chip.GetChipId();
            Debug.Log("Deleting " + chId);

            _numberViews.Remove(chId, out ChipCtrl chipCtrl);
            chipCtrl.gameObject.SetActive(false);  // Return to the pool
        }

        private void ShowEffect(ChipMoveEffect chipMoveEffect)
        {
            int chId = chipMoveEffect.Chip.GetChipId();
            Debug.Log("Moving " + chId + " to " + chipMoveEffect.PointTo);
            ChipCtrl chipCtrl = _numberViews[chId];
            chipCtrl.gameObject.transform.SetPositionAndRotation(
                LogicalToWorld(chipMoveEffect.PointTo), 
                Quaternion.identity
            );
        }

        private void ShowEffect(ChipsMergeEffect chipsMergeEffect)
        {
            int chIdFrom = chipsMergeEffect.ChipFrom.GetChipId();
            int chIdTo = chipsMergeEffect.ChipTo.GetChipId();

            Debug.Log("Merging " + chIdFrom + " with " + chIdTo);
        }


        private void ShowEffect(ChipNumberChangedEffect chipNumberChangedEffect)
        {
            int chId = chipNumberChangedEffect.Chip.GetChipId();
            int newVal = chipNumberChangedEffect.Chip.GetNumericValue();

            Debug.Log("Changed value of " + chId + " to " + newVal);
        }

        private Vector3 LogicalToWorld(Vector2Int logicalPosition)
        {
            return _cellSize * new Vector3(
                _cellSize * (1 + logicalPosition.x) - _horAlgn,
                _cellSize * (1 + logicalPosition.y) - _vertAlgn
            );

        }

    }
}