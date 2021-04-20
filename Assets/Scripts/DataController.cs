using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataController : MonoBehaviour
{
    public string levelName;
    public TilesList tiles;

    private GameObject grid;
    private Tilemap floor, walls;


    private void Start() {
        grid = GameObject.Find("Grid");
        floor = grid.transform.Find("Floor").GetComponent<Tilemap>();
        walls = grid.transform.Find("Walls").GetComponent<Tilemap>();
    }

    public void SaveData() {
        List<Vector2> wallPositions = new List<Vector2>();

        foreach (Vector3Int position in walls.cellBounds.allPositionsWithin)
        {
            if (walls.GetTile(position) != null)
            {
                wallPositions.Add((Vector2)(Vector3)position);
            }
        }

        List<Vector2> floorPositions = new List<Vector2>();

        foreach (Vector3Int position in floor.cellBounds.allPositionsWithin)
        {
            if (floor.GetTile(position) != null)
            {
                floorPositions.Add((Vector2)(Vector3)position);
            }
        }

        LevelData levelData = new LevelData();
        levelData.levelName = levelName;
        levelData.wallPositions = wallPositions.ToArray();
        levelData.floorPositions = floorPositions.ToArray();

        string levelDataJson = JsonUtility.ToJson(levelData, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Levels/" + levelName + ".json", levelDataJson, System.Text.Encoding.UTF8);
    }

    public void LoadData() {
        LevelData levelData = new LevelData();
        string readData = System.IO.File.ReadAllText(Application.persistentDataPath + "/Levels/" + levelName + ".json",
                                     System.Text.Encoding.UTF8);
        levelData = JsonUtility.FromJson<LevelData>(readData);
        levelName = levelData.levelName;

        floor.ClearAllTiles();
        foreach (Vector2 position in levelData.floorPositions)
        {
            floor.SetTile(Vector3Int.FloorToInt((Vector3)position), tiles.wallTile);
        }

        walls.ClearAllTiles();
        foreach (Vector2 position in levelData.wallPositions)
        {
            walls.SetTile(Vector3Int.FloorToInt((Vector3)position), tiles.wallTile);
        }
    }
}