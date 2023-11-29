using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using B83.Win32;
using LevelData;
using System.IO;

namespace Mocked2048Game
{
    public class DragAndDropLevel : MonoBehaviour
    {
        [SerializeField] private GameLevelController _gameLevelController;

        List<string> log = new List<string>();
        void OnEnable()
        {
            // must be installed on the main thread to get the right thread id.
            UnityDragAndDropHook.InstallHook();
            UnityDragAndDropHook.OnDroppedFiles += OnFiles;
        }
        void OnDisable()
        {
            UnityDragAndDropHook.UninstallHook();
        }

        void OnFiles(List<string> aFiles, POINT aPos)
        {
            // do something with the dropped file names. aPos will contain the 
            // mouse position within the window where the files has been dropped.
            //string str = "Dropped " + aFiles.Count + " files at: " + aPos + "\n\t" +
            //    aFiles.Aggregate((a, b) => a + "\n\t" + b);
            //Debug.Log(str);
            //log.Add(str);

            string filePath = aFiles[0];
            string str = "Reading level from " + filePath;
            Debug.Log(str);
            log.Add(str);

            StreamReader reader = new StreamReader(filePath);
            string json = reader.ReadToEnd();

            BoardData level = JsonUtility.FromJson<BoardData>(json);
            _gameLevelController.ResetLevelWithData(level);

        }

        private void OnGUI()
        {
            if (GUILayout.Button("clear log"))
                log.Clear();
            foreach (var s in log)
                GUILayout.Label(s);
        }
    }
}