using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataController : MonoBehaviour
{
    public string levelName;

    public void SaveData() {
        LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
        levelData.SaveDataFromScene();
        string levelDataJson = JsonUtility.ToJson(levelData, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Levels/" + levelName + ".json", levelDataJson);
    }

    public void LoadData() {
        LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
        levelData = JsonUtility.FromJson<LevelData>(Application.persistentDataPath + "/Levels/" + levelName + ".json");
        levelData.name = levelName;
        levelData.LoadDataToScene();
    }
}
