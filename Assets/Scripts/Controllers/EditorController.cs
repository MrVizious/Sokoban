using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class EditorController : MonoBehaviour
{

    [Header("Prefabs")]
    public TilesList tiles;
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject platformPrefab;

    [Header("Menu references")]
    public Image wallSpriteRenderer;
    public Image floorSpriteRenderer;
    public Image playerSpriteRenderer;
    public Image boxSpriteRenderer;
    public Image platformSpriteRenderer;

    [SerializeField] private float maxZoom = 2f;
    [SerializeField] private float zoomStep = 0.05f;
    // Level references
    private GameObject grid;
    private Tilemap wall, floor;

    private void Start() {

        // Get level references
        grid = GameObject.Find("Grid");
        floor = grid.transform.Find("Floor").GetComponent<Tilemap>();
        wall = grid.transform.Find("Walls").GetComponent<Tilemap>();

        // Populate buttons with images
        wallSpriteRenderer.sprite = tiles.wallTile.sprite;
        wallSpriteRenderer.color = tiles.wallTile.color;
        floorSpriteRenderer.sprite = tiles.floorTile.sprite;
        floorSpriteRenderer.color = tiles.floorTile.color;
        playerSpriteRenderer.sprite = playerPrefab.GetComponent<SpriteRenderer>().sprite;
        playerSpriteRenderer.color = playerPrefab.GetComponent<SpriteRenderer>().color;
        boxSpriteRenderer.sprite = boxPrefab.GetComponent<SpriteRenderer>().sprite;
        boxSpriteRenderer.color = boxPrefab.GetComponent<SpriteRenderer>().color;
        platformSpriteRenderer.sprite = platformPrefab.GetComponent<SpriteRenderer>().sprite;
        platformSpriteRenderer.color = platformPrefab.GetComponent<SpriteRenderer>().color;
    }

    private void Update() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            ZoomIn();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // forward
        {
            ZoomOut();
        }
    }

    public void ZoomIn() {
        if (grid.transform.localScale.x <= maxZoom)
        {
            grid.transform.localScale += new Vector3(zoomStep, zoomStep, 0f);
        }
    }
    public void ZoomOut() {
        if (grid.transform.localScale.x >= 1f / maxZoom)
        {
            grid.transform.localScale -= new Vector3(zoomStep, zoomStep, 0f);
        }
    }

}
