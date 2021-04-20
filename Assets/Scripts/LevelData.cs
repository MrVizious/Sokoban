using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LevelData
{
    public string levelName;

    // Topology
    public Vector2[] wallPositions;
    public Vector2[] floorPositions;

    // GameObjects data
    public Vector2[] boxPositions;
    public Vector2[] platformPositions;
    public Vector2 playerPosition;
}