using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelData;
using GameCoreController;
using TMPro;

namespace DebugCoreClient
{
    public class DebugStarter : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private RectTransform _goalsGrid;
        [SerializeField] private GameObject _goalGridElementPrefab;
        [SerializeField] private TextMeshProUGUI _turnsLeftTMP;
        [SerializeField] private RectTransform _boardSafeBounds;
        private Dictionary<string, GoalVisCntr> _goalVisuals;

        void Start()
        {
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

        public Rect CalcBoardBounds()
        {
            float rectWidth = (_boardSafeBounds.anchorMax.x - _boardSafeBounds.anchorMin.x) * Screen.width;
            float rectHeight = (_boardSafeBounds.anchorMax.y - _boardSafeBounds.anchorMin.y) * Screen.height;
            Vector2 position = new Vector2(_boardSafeBounds.anchorMin.x * Screen.width, _boardSafeBounds.anchorMin.y * Screen.height);
            return new Rect(position.x, position.y, rectWidth, rectHeight);
        }

        public void RestartGame()
        {
            _levelController.Reset2048Game();
            InitGoals();
            _levelController.EnableGameUpdateLoop();
        }

        public void End2048Game()
        {
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
