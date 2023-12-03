using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelData;

namespace GameCoreController
{
    public class DebugStarter : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;

        void Start()
        {
            // Start2048Game / Restart2048Game / End2048Game
            _levelController.Start2048Game(LevelsMadeByUra.Level0());

            _levelController.GetBoardController().OnGoalUpdateEvent += OnGoalUpdate;
            _levelController.GetBoardController().OnLooseEvent += OnLoose;
            _levelController.GetBoardController().OnTurnsLeftUpdateEvent += OnTurnsLeftUpdate;
            _levelController.GetBoardController().OnWinEvent += OnWin;
        }

        private void OnDestroy()
        {
            // This is ugly..
            _levelController.GetBoardController().OnGoalUpdateEvent -= OnGoalUpdate;
            _levelController.GetBoardController().OnLooseEvent -= OnLoose;
            _levelController.GetBoardController().OnTurnsLeftUpdateEvent -= OnTurnsLeftUpdate;
            _levelController.GetBoardController().OnWinEvent -= OnWin;
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 100, 150, 50), "Start New"))
            {
                _levelController.Start2048Game(LevelsMadeByUra.Level0());
            } 
            else if (GUI.Button(new Rect(210, 100, 150, 50), "Restart"))
            {
                _levelController.Restart2048Game();
            }
            else if (GUI.Button(new Rect(410, 100, 150, 50), "End"))
            {
                _levelController.End2048Game();
            }

        }

        public void OnGoalUpdate(object sender, GoalUpdateEventArgs args)
        {
            Debug.Log("Goal updated");
        }

        public void OnWin(object sender, WinEventArgs args)
        {
            Debug.Log("You won");
        }

        public void OnLoose(object sender, LooseEventArgs args)
        {
            if (args.IsNoSpawnLeft)
            {
                Debug.Log("You lost - no spawn places left");
            } else if (args.IsNoMoreMoves)
            {
                Debug.Log("You lost - no turns left");
            }
            
        }

        public void OnTurnsLeftUpdate(object sender, TurnsLeftUpdateEventArgs args)
        {
            Debug.Log("Turns left " + args.TurnsLeft);
        }

    }
}
