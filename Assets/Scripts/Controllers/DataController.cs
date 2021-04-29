using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class DataController : MonoBehaviour
{
    public TilesList tiles;
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject platformPrefab;

    public string dataPath = "";

    [HideInInspector] public string levelName;
    private static DataController instance = null;
    [HideInInspector] public static DataController Instance { get { return instance; } }
    public GameObject grid;
    public Tilemap wall, floor;

    private void Awake() {
        instance = this;
        if (dataPath.Equals("")) dataPath = Application.dataPath + "/Resources/Levels/";
        //dataPath = Application.persistentDataPath + "/Levels/";
    }

    private void Start() {
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

        // Get Player Positions
        List<Vector2> playerPositions = new List<Vector2>();
        List<Player> playerComponents = grid.transform.GetComponentsInChildren<Player>().ToList<Player>();
        foreach (Player player in playerComponents)
        {
            playerPositions.Add(player.transform.position);
        }

        // Checks
        // There are enough boxes for the platforms
        if (platformPositions.Count > boxPositions.Count)
        {
            Debug.LogError("There are more platforms than boxes, the level didn't get saved");
            return;
        }
        // There is at least one player
        if (playerPositions.Count <= 0)
        {
            Debug.LogError("There are no players on the scene, the level didn't get saved");
            return;
        }

        // Create class instance to store data
        LevelData levelData = new LevelData();

        // Fill new instance with data
        if (levelName == null || levelName == "") levelName = "noName";
        levelData.levelName = levelName;
        levelData.wallPositions = wallPositions.ToArray();
        levelData.floorPositions = floorPositions.ToArray();
        levelData.boxPositions = boxPositions.ToArray();
        levelData.platformPositions = platformPositions.ToArray();
        levelData.playerPositions = playerPositions.ToArray();

        // Save to JSON file
        string levelDataJson = JsonUtility.ToJson(levelData, true);
        System.IO.File.WriteAllText(dataPath + levelName + ".json", levelDataJson, System.Text.Encoding.UTF8);
    }

    public void LoadData(string newLevelName) {
        levelName = newLevelName;
        LoadData();
    }
    public void LoadData() {
        // Create empty instance
        LevelData levelData = new LevelData();

        // Read JSON data from file
        string readData = System.IO.File.ReadAllText(dataPath + levelName + ".json",
                                     System.Text.Encoding.UTF8);

        // Assign read data to instance
        levelData = JsonUtility.FromJson<LevelData>(readData);

        // Load name
        levelName = levelData.levelName;

        //****************
        // DELETE SECTION
        //****************

        // Delete Floor
        floor.ClearAllTiles();

        // Delete Walls
        wall.ClearAllTiles();

        // Delete Platforms
        List<Platform> platformComponents = grid.GetComponentsInChildren<Platform>().ToList<Platform>();
        foreach (Platform platform in platformComponents)
        {
            platform.enabled = false;
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
            {
                DestroyImmediate(platform.gameObject);
            }
            else
            {
                Destroy(platform.gameObject);
            }
#else
                Destroy(platform.gameObject);
#endif
        }

        // Delete Boxes
        List<Box> boxComponents = grid.GetComponentsInChildren<Box>().ToList<Box>();
        foreach (Box box in boxComponents)
        {
            box.enabled = false;
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
            {
                DestroyImmediate(box.gameObject);
            }
            else
            {
                Destroy(box.gameObject);
            }
#else
                Destroy(box.gameObject);
#endif
        }

        // Delete Players
        List<Player> playerComponents = grid.GetComponentsInChildren<Player>().ToList<Player>();
        foreach (Player player in playerComponents)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
            {
                DestroyImmediate(player.gameObject);
            }
            else
            {
                Destroy(player.gameObject);
            }
#else
                Destroy(player.gameObject);
#endif
        }

        //****************
        // LOADING SECTION
        //****************

        // Load floor tiles
        foreach (Vector2 position in levelData.floorPositions)
        {
            floor.SetTile(Vector3Int.FloorToInt((Vector3)position), tiles.floorTile);
        }

        // Load wall tiles
        foreach (Vector2 position in levelData.wallPositions)
        {
            wall.SetTile(Vector3Int.FloorToInt((Vector3)position), tiles.wallTile);
        }

        // Load platforms
        foreach (Vector2 position in levelData.platformPositions)
        {
            Instantiate(platformPrefab, position, Quaternion.identity, grid.transform);
        }

        // Load boxes
        foreach (Vector2 position in levelData.boxPositions)
        {
            Instantiate(boxPrefab, position, Quaternion.identity, grid.transform);
        }

        // Load players
        foreach (Vector2 position in levelData.playerPositions)
        {
            Instantiate(playerPrefab, position, Quaternion.identity, grid.transform);
        }
    }

    public void setLevelName(string newLevelName) {
        levelName = newLevelName;
    }
}