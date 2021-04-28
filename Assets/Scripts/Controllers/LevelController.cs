using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelController : MonoBehaviour
{
    public bool debug = false;
    public List<string> levels;
    private static LevelController instance;
    [SerializeField] private List<Platform> platforms;
    private int currentLevelIndex;

    [HideInInspector] public static LevelController Instance { get { return instance; } }

    private void Awake() {
        instance = this;
        platforms = new List<Platform>();
    }

    private void Start() {
        currentLevelIndex = 0;
        LoadCurrentLevel();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadCurrentLevel();
        }
    }

    public bool TrackPlatform(Platform newPlatform) {
        if (debug) Debug.Log("Trying to add platform " + newPlatform + " to LevelController", newPlatform);
        if (!platforms.Contains(newPlatform))
        {
            if (debug) Debug.Log(newPlatform + " added!");
            platforms.Add(newPlatform);
            return true;
        }
        else
        {
            if (debug) Debug.Log(newPlatform + " couldn't be added");
            return false;
        }
    }

    public bool UntrackPlatform(Platform newPlatform) {
        if (debug) Debug.Log("Trying to remove plattform " + newPlatform + " to LevelController", newPlatform);
        if (platforms.Contains(newPlatform))
        {
            if (debug) Debug.Log(newPlatform + " removed!");
            platforms.RemoveAll(item => item == null);
            platforms.Remove(newPlatform);
            if (CheckLevelCompleted())
            {
                Debug.Log("Level Completed!");
                LoadNextLevel();
            }
            return true;
        }
        else
        {
            if (debug) Debug.Log(newPlatform + " couldn't be removed");
            return false;
        }
    }

    public bool CheckLevelCompleted() {
        return platforms.Count <= 0;
    }

    public void LoadNextLevel() {
        if (currentLevelIndex < levels.Count)
        {
            if (debug) Debug.Log("Loading next level!");
            currentLevelIndex++;
            LoadCurrentLevel();
        }
        else
        {
            Debug.Log("Game finished!");
        }
    }

    public void LoadCurrentLevel() {
        if (debug) Debug.Log("Loading " + levels[currentLevelIndex] + "!");
        platforms.Clear();
        DataController.Instance.LoadData(levels[currentLevelIndex]);
    }
}

