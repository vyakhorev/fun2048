using System.Collections.Generic;
using UnityEngine;
using LevelData;
using GameCoreController;
using TMPro;
using Meta.Level;
using VContainer;
using System;

namespace DebugCoreClient
{
    public class DebugStarter : MonoBehaviour
    {
        public event Action OnWinEvt;
        public event Action OnLossEvt;

        [SerializeField] private LevelController _levelController;
        [SerializeField] private RectTransform _goalsGrid;
        [SerializeField] private GameObject _goalGridElementPrefab;
        [SerializeField] private TextMeshProUGUI _turnsLeftTMP;
        [SerializeField] private RectTransform _boardSafeBounds;

#if UNITY_EDITOR
        [SerializeField] private bool DebugRegime;
        [SerializeField] private int LevelDebug;
#endif

        private Dictionary<string, GoalVisCntr> _goalVisuals;

        private void Start()
        {
#if UNITY_EDITOR
            if (DebugRegime) RunGame(LevelsMadeByUra.LevelByNumber(LevelDebug));
#endif
        }

        public void RunGame(RootLevelData rootLevelData)
        {
            // Fisrt Setup2048Game, then read goals, then EnableGameUpdateLoop
            _levelController.Setup2048Game(
                rootLevelData,
                CalcBoardBounds()
            );

            InitGoals();

            _levelController.GetBoardController().OnGoalUpdateEvent += OnGoalUpdate;
            _levelController.GetBoardController().OnLooseEvent += OnLoose;
            _levelController.GetBoardController().OnTurnsLeftUpdateEvent += OnTurnsLeftUpdate;
            _levelController.GetBoardController().OnWinEvent += OnWin;

            _levelController.EnableGameUpdateLoop();
        }

        public Vector3[] CalcBoardBounds()
        {
            Vector3[] v = new Vector3[4];
            _boardSafeBounds.GetWorldCorners(v);
            return v;
        }

        public void RestartGame()
        {
            Debug.Log("Restarting the field");
            _levelController.Reset2048Game();
            InitGoals();
            _levelController.EnableGameUpdateLoop();
        }

        private void InitGoals()
        {
            foreach (RectTransform child in _goalsGrid)
            {
                Destroy(child.gameObject);
            }

            _goalVisuals = new Dictionary<string, GoalVisCntr>();
            foreach (var goalView in _levelController.GetBoardController().GetGoalViews())
            {
                GameObject goalGridElement = Instantiate(_goalGridElementPrefab, _goalsGrid);
                GoalVisCntr goalVisCntr = goalGridElement.GetComponent<GoalVisCntr>();
                goalVisCntr.SetupGoal(goalView.Value.GoalSprite, goalView.Value.GoalCounter);
                _goalVisuals[goalView.Key] = goalVisCntr;
            }
        }

        public void OnGoalUpdate(object sender, GoalUpdateEventArgs args)
        {
            _goalVisuals[args.GoalId].UpdateGoal(args.Cnt);

        }

        public void OnWin(object sender, WinEventArgs args)
        {
            _levelController.End2048Game();
            Debug.Log("You won, restarting");
            OnWinEvt.Invoke();
        }

        public void OnLoose(object sender, LooseEventArgs args)
        {
            if (args.IsNoSpawnLeft)
            {
                Debug.Log("You lost - no spawn places left, restarting");
            } else if (args.IsNoMoreMoves)
            {
                Debug.Log("You lost - no turns left, restarting");
            }
            _levelController.End2048Game();
            OnLossEvt.Invoke();
        }

        public void OnTurnsLeftUpdate(object sender, TurnsLeftUpdateEventArgs args)
        {
            _turnsLeftTMP.text = args.TurnsLeft.ToString();
        }
    }
}