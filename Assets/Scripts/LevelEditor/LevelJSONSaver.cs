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

            jsonString = JsonUtility.ToJson(LevelsMadeByUra.Level1(), true);
            saveFile = Application.persistentDataPath + "/level1.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(LevelsMadeByUra.Level2(), true);
            saveFile = Application.persistentDataPath + "/level2.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(LevelsMadeByUra.Level3(), true);
            saveFile = Application.persistentDataPath + "/level3.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(LevelsMadeByUra.Level4(), true);
            saveFile = Application.persistentDataPath + "/level4.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(LevelsMadeByUra.Level5(), true);
            saveFile = Application.persistentDataPath + "/level5.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(LevelsMadeByUra.Level6(), true);
            saveFile = Application.persistentDataPath + "/level6.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(LevelsMadeByUra.Level6(), true);
            saveFile = Application.persistentDataPath + "/level6.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(LevelsMadeByUra.Level7(), true);
            saveFile = Application.persistentDataPath + "/level7.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(LevelsMadeByUra.Level8(), true);
            saveFile = Application.persistentDataPath + "/level8.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(LevelsMadeByUra.Level9(), true);
            saveFile = Application.persistentDataPath + "/level9.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

        }
    }
}
