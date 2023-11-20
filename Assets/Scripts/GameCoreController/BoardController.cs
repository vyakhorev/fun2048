using Mocked2048Game;
using Pooling;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem.HID;

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
        private Dictionary<int, GameObject> _chipViews;

        private Camera _camera;
        private float _cellSize;
        private float _vertAlgn;
        private float _horAlgn;

        public void Init(Transform boardParentTransform)
        {
            _boardParentTransform = boardParentTransform;
            _enquedSwipes = new List<GridDirection>();
            _readyToPlay = false;
            _pool = new GameObjectPools(_boardParentTransform, 10);
            // Warm-up pools
            _pool.EnsurePoolDefinition(GameAssetsManager.Instance.GetGridCellPrefab(), 16);
            _pool.EnsurePoolDefinition(GameAssetsManager.Instance.GetNumberChipPrefab(), 16);
            _camera = Camera.main;
        }

        public void StartNewGame()
        {
            _chip2048Game = new Chip2048Game(4, 4);
            _chip2048Game.ResetGame();
            _chipViews = new Dictionary<int, GameObject>();
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

            // Ensure we collect effects after we apply input
            foreach (GridDirection gridDirection in _enquedSwipes)
            {
                _chip2048Game.TrySwipe(gridDirection);
            }
            // we may miss a few fast swipes cause of this
            _enquedSwipes.Clear();  

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
            foreach (KeyValuePair<int, GameObject> entry in _chipViews)
            {
                entry.Value.SetActive(false);  // Return to the pool
            }
            _chipViews.Clear();
        }

        private void ShowEffect(ChipSpawnedEffect chipSpawnedEffect)
        {
            int chId = chipSpawnedEffect.SpawnedChip.GetChipId();
            Debug.Log("Spawning " + chId + " at " + chipSpawnedEffect.Coords);

            GameObject numberChipPrefab = GameAssetsManager.Instance.GetNumberChipPrefab();
            GameObject chipGo = _pool.PoolObject(numberChipPrefab);
            chipGo.SetActive(true);
            chipGo.transform.SetPositionAndRotation(
                LogicalToWorld(chipSpawnedEffect.Coords), 
                Quaternion.identity
            );
            _chipViews[chId] = chipGo;
        }

        private void ShowEffect(ChipDeletedEffect chipDeletedEffect)
        {
            int chId = chipDeletedEffect.Chip.GetChipId();
            Debug.Log("Deleting " + chId);

            _chipViews.Remove(chId, out GameObject chipGo);
            chipGo.SetActive(false);  // Return to the pool
        }

        private void ShowEffect(ChipMoveEffect chipMoveEffect)
        {
            int chId = chipMoveEffect.Chip.GetChipId();
            Debug.Log("Moving " + chId + " to " + chipMoveEffect.PointTo);
            GameObject chipGo = _chipViews[chId];
            chipGo.transform.SetPositionAndRotation(
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