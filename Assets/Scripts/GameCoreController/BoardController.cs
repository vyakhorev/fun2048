using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using LevelData;
using System.Collections.Specialized;

/*
 * Responsible for board visualisation
 */
namespace GameCoreController
{
    public class BoardController
    {
        /*
         * Public events
         */
        public delegate void OnGoalUpdate(object sender, GoalUpdateEventArgs e);
        public event OnGoalUpdate OnGoalUpdateEvent;

        public delegate void OnWin(object sender, WinEventArgs e);
        public event OnWin OnWinEvent;

        public delegate void OnLoose(object sender, LooseEventArgs e);
        public event OnLoose OnLooseEvent;

        public delegate void OnTurnsLeftUpdate(object sender, TurnsLeftUpdateEventArgs e);
        public event OnTurnsLeftUpdate OnTurnsLeftUpdateEvent;

        // 
        private Chip2048Game _chip2048Game;

        private bool _readyToPlay = false;
        private ChipProducer _chipProducer;

        private Dictionary<Vector2Int, GridCellCtrl> _gridCellViews;
        private Dictionary<int, ChipCtrl> _chipViews;
        private SortedDictionary<string, GameGoalView> _goalViews;

        private bool _animationLock;

        private GridDirection _activeSwipe;
        private Vector2Int _activeTap;
        private bool _hadSwipe;
        private bool _hadTap;

        public void Init(ChipProducer chipProducer)
        {
            _hadSwipe = false;
            _hadTap = false;
            _chipProducer = chipProducer;

            _chip2048Game = new Chip2048Game();

            _chipViews = new Dictionary<int, ChipCtrl>();
            _gridCellViews = new Dictionary<Vector2Int, GridCellCtrl>();

            _goalViews = new SortedDictionary<string, GameGoalView>();

            _readyToPlay = false;
            _animationLock = false;
        }

        public void EnableGameUpdateLoop()
        {
            _readyToPlay = true;
        }

        public void DisableGameUpdateLoop()
        {
            _readyToPlay = false;
        }

        public void SetupNewGame(RootLevelData levelData)
        {
            ClearBoard();

            var boardSize = levelData.Board.BoardSize;
            _chipProducer.InitNewGame(boardSize);

            _chip2048Game.ResetGame(levelData);
            InitGoalViews(
                _chip2048Game.GetGameGoalWatcher(),
                _chipProducer
            );

            _chipViews = new Dictionary<int, ChipCtrl>();
            _gridCellViews = new Dictionary<Vector2Int, GridCellCtrl>();
        }

        public void ExecuteSwipe(GridDirection gridDirection)
        {
            if (!_readyToPlay) return;
            _activeSwipe = gridDirection;
            _hadSwipe = true;
        }

        public void ExecuteTap(Vector2 tapWorldPosition)
        {
            if (!_readyToPlay) return;
            Vector2Int tapLogicalPosition = _chipProducer.WorldToLogical(tapWorldPosition);
            _activeTap = tapLogicalPosition;
            _hadTap = true;
        }

        public void DoUpdate()
        {
            if (!_readyToPlay) return;
            if (_animationLock) return;

            if (_hadTap)
            {
                _chip2048Game.TryTap(_activeTap);
                _hadTap = false;
                _hadSwipe = false;
            }
            else if (_hadSwipe)
            {
                _chip2048Game.TrySwipe(_activeSwipe);
                _hadTap = false;
                _hadSwipe = false;
            }
            
            if (_chip2048Game.ShouldCheckForEffects())
            {
                List<AGridEffect> effects = _chip2048Game.CollectEffects();
                if (effects.Count > 0)
                {
                    _animationLock = true;
                    CoroutineRunEffects(effects);
                }
            }
        }

        public SortedDictionary<string, GameGoalView> GetGoalViews()
        {
            return _goalViews;
        }

        private void InitGoalViews(GameGoalWatcher goalWatcher, ChipProducer chipProducer)
        {
            if (chipProducer.SOBoardVisualStyle == null)
            {
                throw new System.Exception("Cannot InitGoalViews until chipProducer.SOBoardVisualStyle is set");
            }

            if (goalWatcher.GrassGoal > 0)
            {
                _goalViews["grass"] = new GameGoalView
                {
                    GoalSprite = chipProducer.SOBoardVisualStyle.GrassGoalSprite,
                    GoalCounter = goalWatcher.GrassGoal
                };
            }
            if (goalWatcher.HoneyGoal > 0)
            {
                _goalViews["honey"] = new GameGoalView
                {
                    GoalSprite = chipProducer.SOBoardVisualStyle.HoneyGoalSprite,
                    GoalCounter = goalWatcher.HoneyGoal
                };
            }
            if (goalWatcher.EggGoal > 0)
            {
                _goalViews["eggs"] = new GameGoalView
                {
                    GoalSprite = chipProducer.SOBoardVisualStyle.EggGoalSprite,
                    GoalCounter = goalWatcher.EggGoal
                };
            }
            if (goalWatcher.BoxGoal > 0)
            {
                _goalViews["boxes"] = new GameGoalView
                {
                    GoalSprite = chipProducer.SOBoardVisualStyle.BoxGoalSprite,
                    GoalCounter = goalWatcher.BoxGoal
                };
            }
            if (goalWatcher.BombGoal > 0)
            {
                _goalViews["bombs"] = new GameGoalView
                {
                    GoalSprite = chipProducer.SOBoardVisualStyle.BombGoalSprite,
                    GoalCounter = goalWatcher.BombGoal
                };
            }
            foreach (var numberGoal in goalWatcher.NumberComb)
            {
                var sprite = chipProducer.SOBoardVisualStyle.GetNumberSprite(numberGoal.Key);
                _goalViews[numberGoal.Key.ToString()] = new GameGoalView
                {
                    GoalSprite = sprite,
                    GoalCounter = numberGoal.Value
                };

            }
        }

        // Order is quite important here
        private async void CoroutineRunEffects(List<AGridEffect> effects)
        {
            foreach (var eff in effects.OfType<BoardResetEffect>())
            {
                ResetBoard(eff);
            }

            foreach (var eff in effects.OfType<ChipSpawnedEffect>())
            {
                SpawnNewChip(eff);
            }

            foreach (var eff in effects.OfType<CellEnabledChangeEffect>())
            {
                ShowEffect(eff);
            }

            Sequence tweenSeq = DOTween.Sequence();
            foreach (var eff in effects.OfType<BoardResetEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }

            foreach (var eff in effects.OfType<ChipMoveEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }

            foreach (var eff in effects.OfType<ChipNumberChangedEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }

            foreach (var eff in effects.OfType<ChipsMergeEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }

            foreach (var eff in effects.OfType<ChipDeletedEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }

            foreach (var eff in effects.OfType<GrassHealthChangeEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }

            foreach (var eff in effects.OfType<HoneyHealthChangeEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }

            foreach (var eff in effects.OfType<ChipHealthChangeEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }

            foreach (var eff in effects.OfType<ChipSpawnedEffect>())
            {
                ShowEffect(eff, tweenSeq);
            }
            await tweenSeq.AsyncWaitForCompletion();

            foreach (var eff in effects.OfType<ChipDeletedEffect>())
            {
                DeleteChip(eff);
            }

            foreach (var eff in effects.OfType<GoalChangedEffect>())
            {
                ShowEffect(eff);
            }

            foreach (var eff in effects.OfType<GameLostEffect>())
            {
                ShowEffect(eff);
            }

            foreach (var eff in effects.OfType<GameWonEffect>())
            {
                ShowEffect(eff);
            }

            foreach (var eff in effects.OfType<TurnsLeftChangedEffect>())
            {
                ShowEffect(eff);
            }

            _animationLock = false;
        }

        private void SpawnNewChip(ChipSpawnedEffect chipSpawnedEffect)
        {
            int chId = chipSpawnedEffect.SpawnedChip.GetChipId();

            if (chipSpawnedEffect.SpawnedChip is NumberChip numberChip)
            {
                ChipCtrl chipCtrl = _chipProducer.SpawnNumberChip(
                    chipSpawnedEffect.Coords,
                    numberChip.GetNumericValue()
                );
                _chipViews[chId] = chipCtrl;
            }
            else if (chipSpawnedEffect.SpawnedChip is BoxChip stoneChip)
            {
                ChipCtrl chipCtrl = _chipProducer.SpawnBoxChip(
                    chipSpawnedEffect.Coords,
                    stoneChip.GetHealth()
                );
                _chipViews[chId] = chipCtrl;
            }
            else if (chipSpawnedEffect.SpawnedChip is EggChip eggChip)
            {
                ChipCtrl chipCtrl = _chipProducer.SpawnEggChip(
                    chipSpawnedEffect.Coords,
                    eggChip.GetHealth()
                );
                _chipViews[chId] = chipCtrl;
            }
            else if (chipSpawnedEffect.SpawnedChip is BubbleChip bubbleChip)
            {
                ChipCtrl chipCtrl = _chipProducer.SpawnBubbleChip(
                    chipSpawnedEffect.Coords,
                    bubbleChip.GetBubbleValue()
                );
                _chipViews[chId] = chipCtrl;
            }
            else if (chipSpawnedEffect.SpawnedChip is BombChip bombChip)
            {
                ChipCtrl chipCtrl = _chipProducer.SpawnBombChip(
                    chipSpawnedEffect.Coords
                );
                _chipViews[chId] = chipCtrl;
            }
        }

        private void DeleteChip(ChipDeletedEffect chipDeletedEffect)
        {
            int chId = chipDeletedEffect.Chip.GetChipId();

            _chipViews.Remove(chId, out ChipCtrl chipCtrl);
            chipCtrl.gameObject.SetActive(false);  // Return to the pool
        }

        public void ClearBoard()
        {
            foreach (KeyValuePair<int, ChipCtrl> entry in _chipViews)
            {
                entry.Value.gameObject.SetActive(false);  // Return to the pool
            }
            _chipViews.Clear();

            // TODO: maybe do not reset?
            foreach (KeyValuePair<Vector2Int, GridCellCtrl> entry in _gridCellViews)
            {
                entry.Value.gameObject.SetActive(false);  // Return to the pool
            }
            _gridCellViews.Clear();
        }

        private void ResetBoard(BoardResetEffect boardResetEffect)
        {
            ClearBoard();

            Vector2Int bs = _chip2048Game.GetBoardSize();
            for (int x = 0; x < bs.x; x++)
            {
                for (int y = 0; y < bs.y; y++)
                {
                    var v = new Vector2Int(x, y);
                    GridCellCtrl gridCellCtrl = _chipProducer.SpawnGridCell(v);
                    _gridCellViews[v] = gridCellCtrl;
                    gridCellCtrl.InitHierarchy();
                    gridCellCtrl.SetEvenBackgroundColor(v);
                }
            }

        }

        private void ShowEffect(BoardResetEffect boardResetEffect, Sequence tweenSeq)
        {

        }

        private void ShowEffect(CellEnabledChangeEffect cellEnabledChangeEffect)
        {
            GridCellCtrl gridCellCtrl = _gridCellViews[cellEnabledChangeEffect.CellCoords];
            if (cellEnabledChangeEffect.IsEnabled)
            {
                gridCellCtrl.SetCellEnabled();
                gridCellCtrl.SetEvenBackgroundColor(cellEnabledChangeEffect.CellCoords);
            } else
            {
                gridCellCtrl.SetCellDisabled();
            }
        }

        private void ShowEffect(GrassHealthChangeEffect grassHealthChangeEffect, Sequence tweenSeq)
        {
            GridCellCtrl gridCellCtrl = _gridCellViews[grassHealthChangeEffect.CellCoords];
            if (grassHealthChangeEffect.HealthLevel == 0)
            {
                gridCellCtrl.RemoveGrass(tweenSeq);
            }
            else if (grassHealthChangeEffect.HealthLevel == 1)
            {
                gridCellCtrl.SetGrassLevel1(tweenSeq);
            }
            else if (grassHealthChangeEffect.HealthLevel >= 2)
            {
                gridCellCtrl.SetGrassLevel2(tweenSeq);
            } else
            {
                throw new System.Exception(
                    "Wrong grass health level " + grassHealthChangeEffect.HealthLevel);
            }
        }

        private void ShowEffect(HoneyHealthChangeEffect honeyHealthChangeEffect, Sequence tweenSeq)
        {
            GridCellCtrl gridCellCtrl = _gridCellViews[honeyHealthChangeEffect.CellCoords];
            if (honeyHealthChangeEffect.HealthLevel == 0)
            {
                gridCellCtrl.RemoveHoney(tweenSeq);
            }
            else if (honeyHealthChangeEffect.HealthLevel >= 1)
            {
                gridCellCtrl.SetHoney(tweenSeq);
            }
            else
            {
                throw new System.Exception(
                    "Wrong honey health level " + honeyHealthChangeEffect.HealthLevel);
            }
        }

        private void ShowEffect(ChipSpawnedEffect chipSpawnedEffect, Sequence tweenSeq)
        {
            int chId = chipSpawnedEffect.SpawnedChip.GetChipId();
            ChipCtrl chipCtrl = _chipViews[chId];

            tweenSeq.Insert(
                0f,
                chipCtrl.transform.DOScale(
                    Vector3.one,
                    _chipProducer.GetAnimSpeed()
                )
            );

        }

        private void ShowEffect(ChipDeletedEffect chipDeletedEffect, Sequence tweenSeq)
        {
            int chId = chipDeletedEffect.Chip.GetChipId();
            ChipCtrl chipCtrl = _chipViews[chId];

            tweenSeq.Insert(
                0f,
                chipCtrl.transform.DOScale(
                    1.3f * Vector3.one,
                    _chipProducer.GetAnimSpeed()
                )
            );

            tweenSeq.Insert(
                _chipProducer.GetAnimSpeed(),
                chipCtrl.transform.DOScale(
                    Vector3.zero,
                    _chipProducer.GetAnimSpeed()
                )
            );
        }

        private void ShowEffect(ChipMoveEffect chipMoveEffect, Sequence tweenSeq)
        {
            int chId = chipMoveEffect.Chip.GetChipId();
            ChipCtrl chipCtrl = _chipViews[chId];

            tweenSeq.Insert(
                0f,
                chipCtrl.transform.DOMove(
                    _chipProducer.LogicalToWorld(chipMoveEffect.PointTo),
                    _chipProducer.GetAnimSpeed()
                )
            );
        }

        private void ShowEffect(ChipsMergeEffect chipsMergeEffect, Sequence tweenSeq)
        {
            int chIdFrom = chipsMergeEffect.ChipFrom.GetChipId();
            int chIdTo = chipsMergeEffect.ChipTo.GetChipId();

            ChipCtrl chipCtrlTo = _chipViews[chIdTo];
            tweenSeq.Insert(
                0f,
                chipCtrlTo.transform.DOScale(
                    Vector3.zero,
                    _chipProducer.GetAnimSpeed()
                )
            );

        }


        private void ShowEffect(ChipNumberChangedEffect chipNumberChangedEffect, Sequence tweenSeq)
        {
            int chId = chipNumberChangedEffect.Chip.GetChipId();
            int newVal = chipNumberChangedEffect.Chip.GetNumericValue();

            ChipCtrl chipCtrl = _chipViews[chId];
            _chipProducer.UpdateNumberVisuals(chipCtrl, newVal);

            tweenSeq.Insert(
                0f,
                chipCtrl.transform.DOScale(
                    1.3f * Vector3.one,
                    _chipProducer.GetAnimSpeed()
                )
            );
            tweenSeq.Insert(
                _chipProducer.GetAnimSpeed(),
                chipCtrl.transform.DOScale(
                    Vector3.one,
                    _chipProducer.GetAnimSpeed()
                )
            );
        }

        private void ShowEffect(ChipHealthChangeEffect chipHealthChangeEffect, Sequence tweenSeq)
        {
            AChip chip = chipHealthChangeEffect.Chip;
            ChipCtrl chipCtrl = _chipViews[chip.GetChipId()];

            if (chip is EggChip eggChip)
            {
                tweenSeq.Insert(
                    0f,
                    chipCtrl.transform.DOScale(
                        1.3f * Vector3.one,
                        _chipProducer.GetAnimSpeed()
                    ).OnComplete(() => chipCtrl.SetEgg(
                        chipHealthChangeEffect.Health
                    ))
                );
                tweenSeq.Insert(
                    _chipProducer.GetAnimSpeed(),
                    chipCtrl.transform.DOScale(
                        Vector3.one,
                        _chipProducer.GetAnimSpeed()
                    )
                );
            }
            else if (chip is BoxChip boxChip)
            {
                tweenSeq.Insert(
                    0f,
                    chipCtrl.transform.DOScale(
                        1.3f * Vector3.one,
                        _chipProducer.GetAnimSpeed()
                    ).OnComplete(() => chipCtrl.SetBox(
                        chipHealthChangeEffect.Health
                    ))
                );
                tweenSeq.Insert(
                    _chipProducer.GetAnimSpeed(),
                    chipCtrl.transform.DOScale(
                        Vector3.one,
                        _chipProducer.GetAnimSpeed()
                    )
                );
            }

        }

        private void ShowEffect(GoalChangedEffect goalChangedEffect)
        {
            string goalId;
            if (goalChangedEffect.Goal == GameGoals.GRASS)
            {
                goalId = "grass";
            }
            else if (goalChangedEffect.Goal == GameGoals.HONEY)
            {
                goalId = "honey";
            }
            else if (goalChangedEffect.Goal == GameGoals.EGG)
            {
                goalId = "eggs";
            }
            else if (goalChangedEffect.Goal == GameGoals.BOX)
            {
                goalId = "boxes";
            }
            else if (goalChangedEffect.Goal == GameGoals.NUMBER)
            {
                goalId = goalChangedEffect.Number.ToString();
            }
            else if (goalChangedEffect.Goal == GameGoals.BOMB)
            {
                goalId = "bombs";
            }
            else
            {
                throw new System.Exception("Unknown goal " + goalChangedEffect.Goal);
            }

            _goalViews[goalId].GoalCounter = goalChangedEffect.NewCnt;
            OnGoalUpdateEvent?.Invoke(
                this, 
                new GoalUpdateEventArgs(
                    goalId,
                    goalChangedEffect.NewCnt
                )
            );

        }

        private void ShowEffect(GameWonEffect gameWonEffect)
        {
            OnWinEvent?.Invoke(
                this,
                new WinEventArgs()
            );
        }

        private void ShowEffect(GameLostEffect gameLostEffect)
        {
            OnLooseEvent?.Invoke(
                this,
                new LooseEventArgs(
                    gameLostEffect.IsNoMoreMoves, 
                    gameLostEffect.IsNoSpawnLeft
                )
            );
        }

        private void ShowEffect(TurnsLeftChangedEffect turnsLeftChangedEffect)
        {
            OnTurnsLeftUpdateEvent?.Invoke(
                this,
                new TurnsLeftUpdateEventArgs(
                    turnsLeftChangedEffect.TurnsLeft
                )
            );
        }

    }
}