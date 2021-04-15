using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string levelName;
    [SerializeField] private GameObject grid;

    public void SaveDataFromScene() {
        grid = GameObject.Find("Grid");
    }

    public void LoadDataToScene() {
        Destroy(GameObject.Find("Grid"));
        Instantiate(grid, grid.transform.position, grid.transform.rotation);
    }
}