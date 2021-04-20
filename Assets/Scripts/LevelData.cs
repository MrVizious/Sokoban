using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public Vector2[] wallPositions;
    public Vector2[] floorPositions;
}