using LevelData;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LevelEditor
{
    public class LevelJSONSaver : MonoBehaviour
    {
        private void Start()
        {
            string jsonString;
            string saveFile;

            for (int i = 1; i <= LevelsMadeByUra.HowManyLevels(); i++)
            {
                jsonString = JsonUtility.ToJson(LevelsMadeByUra.LevelByNumber(i), true);
                saveFile = Application.persistentDataPath + "/level" + i.ToString() + ".json";
                File.WriteAllText(saveFile, jsonString);
                Debug.Log("Saving " + saveFile);
            }
        }
    }
}
