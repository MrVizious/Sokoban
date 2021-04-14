using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class LoadableTilemap : MonoBehaviour
{
    [SerializeField]
    public List<Cell> cells;
    [System.Serializable]
    public struct Cell
    {
        public Tile tile;
        public Vector3Int position;
    }
    private Tilemap tilemap;

    private void Start() {
        tilemap = GetComponent<Tilemap>();
        cells = ToCells();
    }

    private List<Cell> ToCells() {

        List<Cell> newCells = new List<Cell>();
        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(position) != null)
            {
                Cell newCell = new Cell();
                newCell.position = position;
                newCell.tile = (Tile)tilemap.GetTile(position);
                newCells.Add(newCell);
            }
        }
        return newCells;
    }

    public void LoadFromCells(List<Cell> newCells) {
        tilemap.ClearAllTiles();
        foreach (Cell cell in newCells)
        {
            tilemap.SetTile(cell.position, cell.tile);
        }
    }
}
