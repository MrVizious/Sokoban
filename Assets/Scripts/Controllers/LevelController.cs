using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public bool debug = false;
    private static LevelController instance;
    private List<Platform> platforms;

    [HideInInspector] public static LevelController Instance { get { return instance; } }

    private void Awake() {
        instance = this;
        platforms = new List<Platform>();
    }

    public bool TrackPlatform(Platform newPlatform) {
        if (debug) Debug.Log("Trying to add plattform " + newPlatform + " to LevelController", newPlatform);
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
            platforms.Remove(newPlatform);
            if (CheckLevelCompleted())
            {
                Debug.Log("Level Completed!");
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
}

