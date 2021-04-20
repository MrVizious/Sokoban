using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataController : MonoBehaviour
{
    public string levelName;
    public TilesList tiles;
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject platformPrefab;


    private GameObject grid;
    private Tilemap floor, wall;


    private void Start() {
        grid = GameObject.Find("Grid");
        floor = grid.transform.Find("Floor").GetComponent<Tilemap>();
        wall = grid.transform.Find("Walls").GetComponent<Tilemap>();
    }

    public void SaveData() {

        // Get Wall Positions
        List<Vector2> wallPositions = new List<Vector2>();
        foreach (Vector3Int position in wall.cellBounds.allPositionsWithin)
        {
            if (wall.GetTile(position) != null)
            {
                wallPositions.Add((Vector2)(Vector3)position);
            }
        }

        // Get Floor Positions
        List<Vector2> floorPositions = new List<Vector2>();
        foreach (Vector3Int position in floor.cellBounds.allPositionsWithin)
        {
            if (floor.GetTile(position) != null)
            {
                floorPositions.Add((Vector2)(Vector3)position);
            }
        }

        // Get Box Positions
        List<Vector2> boxPositions = new List<Vector2>();
        List<Box> boxComponents = grid.transform.GetComponentsInChildren<Box>().ToList<Box>();
        foreach (Box box in boxComponents)
        {
            boxPositions.Add(box.transform.position);
        }

        // Get Platform Positions
        List<Vector2> platformPositions = new List<Vector2>();
        List<Platform> platformComponents = grid.transform.GetComponentsInChildren<Platform>().ToList<Platform>();
        foreach (Platform platform in platformComponents)
        {
            platformPositions.Add(platform.transform.position);
        }

        // Get Player Position
        Vector2 playerPosition = grid.transform.Find("Player").transform.position;

        // Create class instance to store data
        LevelData levelData = new LevelData();

        // Fill new instance with data
        levelData.levelName = levelName;
        levelData.wallPositions = wallPositions.ToArray();
        levelData.floorPositions = floorPositions.ToArray();
        levelData.boxPositions = boxPositions.ToArray();
        levelData.platformPositions = platformPositions.ToArray();
        levelData.playerPosition = playerPosition;

        // Save to JSON file
        string levelDataJson = JsonUtility.ToJson(levelData, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Levels/" + levelName + ".json", levelDataJson, System.Text.Encoding.UTF8);
    }

    public void LoadData() {
        // Create empty instance
        LevelData levelData = new LevelData();

        // Read JSON data from file
        string readData = System.IO.File.ReadAllText(Application.persistentDataPath + "/Levels/" + levelName + ".json",
                                     System.Text.Encoding.UTF8);

        // Assign read data to instance
        levelData = JsonUtility.FromJson<LevelData>(readData);

        // Load name
        levelName = levelData.levelName;

        // Load and substitute floor tiles
        floor.ClearAllTiles();
        foreach (Vector2 position in levelData.floorPositions)
        {
            floor.SetTile(Vector3Int.FloorToInt((Vector3)position), tiles.wallTile);
        }

        // Load and substitute wall tiles
        wall.ClearAllTiles();
        foreach (Vector2 position in levelData.wallPositions)
        {
            wall.SetTile(Vector3Int.FloorToInt((Vector3)position), tiles.wallTile);
        }

        // Load and substitute boxes
        List<Box> boxComponents = grid.GetComponentsInChildren<Box>().ToList<Box>();
        foreach (Box box in boxComponents)
        {
            Destroy(box.gameObject);
        }
        foreach (Vector2 position in levelData.boxPositions)
        {
            Instantiate(boxPrefab, position, Quaternion.identity, grid.transform);
        }

        // Load and substitute platforms
        List<Platform> platformComponents = grid.GetComponentsInChildren<Platform>().ToList<Platform>();
        foreach (Platform platform in platformComponents)
        {
            Destroy(platform.gameObject);
        }
        foreach (Vector2 position in levelData.platformPositions)
        {
            Instantiate(platformPrefab, position, Quaternion.identity, grid.transform);
        }

        // Load and substitute player
        Destroy(grid.GetComponentInChildren<PlayerMovement>().gameObject);
        Instantiate(playerPrefab, levelData.playerPosition, Quaternion.identity, grid.transform);
    }
}