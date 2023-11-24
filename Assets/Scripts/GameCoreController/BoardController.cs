// using Mocked2048Game;
using Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualSO;
using DG.Tweening;
using System.Linq;
using System;

/*
 * Responsible for board visualisation
 */
namespace GameCoreController
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private float _animSpeed = 0.1f;

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

            DOTween.Init();

        }

        public void StartNewGame()
        {
            _chip2048Game = new Chip2048Game(4, 4);
            _chip2048Game.ResetGame();
            _numberViews = new Dictionary<int, ChipCtrl>();
            _gridCellViews = new Dictionary<int, GridCellCtrl>();

            Vector2Int boardSize = _chip2048Game.GetBoardSize();
            float worldHeight = _camera.orthographicSize * 2f;
            float worldWidth = worldHeight * _camera.aspect;
            _horAlgn = worldWidth / 2f;
            _vertAlgn = worldHeight / 2f;

            _cellSize = Mathf.Min(
                worldWidth * 0.8f / boardSize.x,
                worldHeight * 0.8f / boardSize.y
            );

            _readyToPlay = true;

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

            CoroutineRunEffects(_chip2048Game.GetEffects());
            _chip2048Game.ResetEffects();
        }

        // Just a chained call with board reset as a root call
        private async void CoroutineRunEffects(List<AGridEffect> effects)
        {
            Sequence tweenSeq = DOTween.Sequence();
            foreach (var eff in effects.OfType<BoardResetEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }
            await tweenSeq.AsyncWaitForCompletion();

            tweenSeq = DOTween.Sequence();
            foreach (var eff in effects.OfType<ChipMoveEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }
            await tweenSeq.AsyncWaitForCompletion();

            tweenSeq = DOTween.Sequence();
            foreach (var eff in effects.OfType<ChipNumberChangedEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }
            await tweenSeq.AsyncWaitForCompletion();

            tweenSeq = DOTween.Sequence();
            foreach (var eff in effects.OfType<ChipsMergeEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }
            await tweenSeq.AsyncWaitForCompletion();

            tweenSeq = DOTween.Sequence();
            foreach (var eff in effects.OfType<ChipDeletedEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }
            await tweenSeq.AsyncWaitForCompletion();

            tweenSeq = DOTween.Sequence();
            foreach (var eff in effects.OfType<ChipSpawnedEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }
            await tweenSeq.AsyncWaitForCompletion();

        }

        private void ShowEffect(BoardResetEffect boardResetEffect, Sequence tweenSeq)
        {
            Debug.Log("Board reset");
            foreach (KeyValuePair<int, ChipCtrl> entry in _numberViews)
            {
                entry.Value.gameObject.SetActive(false);  // Return to the pool
            }
            _numberViews.Clear();
        }

        private void ShowEffect(ChipSpawnedEffect chipSpawnedEffect, Sequence tweenSeq)
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
            chipGo.transform.localScale = Vector3.zero;
            ChipCtrl chipCtrl = chipGo.GetComponent<ChipCtrl>();
            _numberViews[chId] = chipCtrl;
            if (chipSpawnedEffect.SpawnedChip is NumberChip numberChip)
            {
                _chipProducer.UpdateNumberVisuals(chipCtrl, numberChip.GetNumericValue());
            }

            tweenSeq.Insert(
                0f,
                chipCtrl.transform.DOScale(
                    Vector3.one,
                    _animSpeed
                )
            );

        }

        private void ShowEffect(ChipDeletedEffect chipDeletedEffect, Sequence tweenSeq)
        {
            int chId = chipDeletedEffect.Chip.GetChipId();
            Debug.Log("Deleting " + chId);

            _numberViews.Remove(chId, out ChipCtrl chipCtrl);
            chipCtrl.gameObject.SetActive(false);  // Return to the pool
        }

        private void ShowEffect(ChipMoveEffect chipMoveEffect, Sequence tweenSeq)
        {
            int chId = chipMoveEffect.Chip.GetChipId();
            Debug.Log("Moving " + chId + " to " + chipMoveEffect.PointTo);
            ChipCtrl chipCtrl = _numberViews[chId];

            tweenSeq.Insert(
                0f,
                chipCtrl.transform.DOMove(
                    LogicalToWorld(chipMoveEffect.PointTo),
                    _animSpeed
                )
            );
        }

        private void ShowEffect(ChipsMergeEffect chipsMergeEffect, Sequence tweenSeq)
        {
            int chIdFrom = chipsMergeEffect.ChipFrom.GetChipId();
            int chIdTo = chipsMergeEffect.ChipTo.GetChipId();

            Debug.Log("Merging " + chIdFrom + " with " + chIdTo);
        }


        private void ShowEffect(ChipNumberChangedEffect chipNumberChangedEffect, Sequence tweenSeq)
        {
            int chId = chipNumberChangedEffect.Chip.GetChipId();
            int newVal = chipNumberChangedEffect.Chip.GetNumericValue();

            Debug.Log("Changed value of " + chId + " to " + newVal);

            ChipCtrl chipCtrl = _numberViews[chId];
            _chipProducer.UpdateNumberVisuals(chipCtrl, newVal);
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