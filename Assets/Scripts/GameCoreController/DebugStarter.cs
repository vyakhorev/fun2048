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
            //_levelController.Start2048Game(LevelsMadeByUra.Level0());
            // _levelController.Restart2048Game();
            // _levelController.End2048Game();
        }
    }
}
