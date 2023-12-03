using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelData;
using GameCoreController;
using TMPro;
using UnityEngine.UI;

namespace DebugCoreClient
{
    public class DebugStarter : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private RectTransform _goalsGrid;
        [SerializeField] private GameObject _goalGridElementPrefab;
        [SerializeField] private TextMeshProUGUI _turnsLeftTMP;
        [SerializeField] private VerticalLayoutGroup _groupToWait;
        [SerializeField] private RectTransform _boardSafeBounds;
        private Dictionary<string, GoalVisCntr> _goalVisuals;

        void Start()
        {
            // https://forum.unity.com/threads/force-immediate-layout-update.372630/
            _groupToWait.CalculateLayoutInputHorizontal();
            _groupToWait.CalculateLayoutInputVertical();
            _groupToWait.SetLayoutHorizontal();
            _groupToWait.SetLayoutVertical();

            // Fisrt Setup2048Game, then read goals, then EnableGameUpdateLoop
            _levelController.Setup2048Game(
                LevelsMadeByUra.Level5(),
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

        public void End2048Game()
        {
            Debug.Log("Ending the game, clearing the field");
            _levelController.End2048Game();
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
            Debug.Log("You won, restarting");
            RestartGame();
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
            RestartGame();
        }

        public void OnTurnsLeftUpdate(object sender, TurnsLeftUpdateEventArgs args)
        {
            _turnsLeftTMP.text = args.TurnsLeft.ToString();
        }

    }
}
