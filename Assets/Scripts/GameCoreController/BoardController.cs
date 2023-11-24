// using Mocked2048Game;
using Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualSO;
using DG.Tweening;
using System.Linq;
using System;
using UnityEngine.InputSystem.HID;

/*
 * Responsible for board visualisation
 */
namespace GameCoreController
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private float _animSpeed = 0.1f;

        private Chip2048Game _chip2048Game;

        private List<GridDirection> _enquedSwipes;
        private bool _readyToPlay = false;
        private ChipProducer _chipProducer;

        private Dictionary<int, GridCellCtrl> _gridCellViews;
        private Dictionary<int, ChipCtrl> _numberViews;
       

        public void Init(ChipProducer chipProducer)
        {
            _enquedSwipes = new List<GridDirection>();
            _readyToPlay = false;
            _chipProducer = chipProducer;
        }

        public void StartNewGame(Vector2Int boardSize)
        {
            _chip2048Game = new Chip2048Game(boardSize.x, boardSize.y);
            _chip2048Game.ResetGame();
            _chipProducer.InitNewGame(boardSize);

            _numberViews = new Dictionary<int, ChipCtrl>();
            _gridCellViews = new Dictionary<int, GridCellCtrl>();

            _readyToPlay = true;

        }

        public void ExecuteSwipe(GridDirection gridDirection)
        {
            if (!_readyToPlay) return;
            _enquedSwipes.Add(gridDirection);
        }

        public void Update()
        {
            if (!_readyToPlay) return;
           
            List<GridDirection> acts = new List<GridDirection>(_enquedSwipes);
            _enquedSwipes.Clear();
            foreach (GridDirection gridDirection in acts)
            {
                _chip2048Game.TrySwipe(gridDirection);
            }

            List<AGridEffect> effects = _chip2048Game.GetEffects();
            _chip2048Game.ResetEffects();
            
            if (effects.Count > 0)
            {
                CoroutineRunEffects(effects);
            }
            
        }

        // Order is quite important here
        private async void CoroutineRunEffects(List<AGridEffect> effects)
        {

            Sequence tweenSeq = DOTween.Sequence();
            foreach (var eff in effects.OfType<BoardResetEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }
            await tweenSeq.AsyncWaitForCompletion();

            foreach (var eff in effects.OfType<ChipSpawnedEffect>())
            {
                SpawnNewChip(eff);
            }

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

            foreach (var eff in effects.OfType<ChipDeletedEffect>())
            {
                DeleteChip(eff);
            }

        }

        private void SpawnNewChip(ChipSpawnedEffect chipSpawnedEffect)
        {
            int chId = chipSpawnedEffect.SpawnedChip.GetChipId();

            if (chipSpawnedEffect.SpawnedChip is NumberChip numberChip)
            {
                ChipCtrl chipCtrl = _chipProducer.SpawnChip(
                    chipSpawnedEffect.Coords,
                    numberChip.GetNumericValue()
                );
                _numberViews[chId] = chipCtrl;
            }
        }

        private void DeleteChip(ChipDeletedEffect chipDeletedEffect)
        {
            int chId = chipDeletedEffect.Chip.GetChipId();

            _numberViews.Remove(chId, out ChipCtrl chipCtrl);
            chipCtrl.gameObject.SetActive(false);  // Return to the pool
        }

        private void ShowEffect(BoardResetEffect boardResetEffect, Sequence tweenSeq)
        {
            foreach (KeyValuePair<int, ChipCtrl> entry in _numberViews)
            {
                entry.Value.gameObject.SetActive(false);  // Return to the pool
            }
            _numberViews.Clear();
        }

        private void ShowEffect(ChipSpawnedEffect chipSpawnedEffect, Sequence tweenSeq)
        {
            int chId = chipSpawnedEffect.SpawnedChip.GetChipId();
            ChipCtrl chipCtrl = _numberViews[chId];

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
            ChipCtrl chipCtrl = _numberViews[chId];

            tweenSeq.Insert(
                0f,
                chipCtrl.transform.DOScale(
                    Vector3.zero,
                    _animSpeed
                )
            );
        }

        private void ShowEffect(ChipMoveEffect chipMoveEffect, Sequence tweenSeq)
        {
            int chId = chipMoveEffect.Chip.GetChipId();
            //Debug.Log("Moving " + chId + " to " + chipMoveEffect.PointTo);
            ChipCtrl chipCtrl = _numberViews[chId];

            tweenSeq.Insert(
                0f,
                chipCtrl.transform.DOMove(
                    _chipProducer.LogicalToWorld(chipMoveEffect.PointTo),
                    _animSpeed
                )
            );
        }

        private void ShowEffect(ChipsMergeEffect chipsMergeEffect, Sequence tweenSeq)
        {
            int chIdFrom = chipsMergeEffect.ChipFrom.GetChipId();
            int chIdTo = chipsMergeEffect.ChipTo.GetChipId();

            //Debug.Log("Merging " + chIdFrom + " with " + chIdTo);

            ChipCtrl chipCtrlTo = _numberViews[chIdTo];
            tweenSeq.Insert(
                0f,
                chipCtrlTo.transform.DOScale(
                    Vector3.zero,
                    _animSpeed
                )
            );

        }


        private void ShowEffect(ChipNumberChangedEffect chipNumberChangedEffect, Sequence tweenSeq)
        {
            int chId = chipNumberChangedEffect.Chip.GetChipId();
            int newVal = chipNumberChangedEffect.Chip.GetNumericValue();

            ChipCtrl chipCtrl = _numberViews[chId];
            _chipProducer.UpdateNumberVisuals(chipCtrl, newVal);

            tweenSeq.Insert(
                0f,
                chipCtrl.transform.DOScale(
                    1.2f * Vector3.one,
                    _animSpeed
                )
            );
            tweenSeq.Insert(
                _animSpeed,
                chipCtrl.transform.DOScale(
                    Vector3.one,
                    _animSpeed
                )
            );
        }

    }
}