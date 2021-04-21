using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TilesList", menuName = "ScriptableObjects/TilesList", order = 1)]
public class TilesList : ScriptableObject
{
    public Tile wallTile;
    public Tile floorTile;

}