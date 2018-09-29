using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour {
    public GameObject[] tiles;
    public float spawnAhead = 500f;
    List<GameObject> spawnedTiles;
    public GameObject startTile;
    GameObject lastTile;
    Camera mainCamera;
	// Use this for initialization
	void Awake () {
        mainCamera = Camera.main;
        spawnedTiles = new List<GameObject>();
        spawnedTiles.Add(startTile);
        lastTile = startTile;
        foreach (GameObject g in tiles)
        {
            g.SetActive(false);
        }
        SpawnTile();
	}
	// Update is called once per frame
	void Update () {
        //if distance from last spawned is smaller than spawnAhead, spawn new block
        if (lastTile.transform.position.y - mainCamera.transform.position.y < spawnAhead) 
        {
            SpawnTile();
        }
	}
    void SpawnTile() {
        int tileIndex = Random.Range(0, tiles.Length);
        GameObject newTile = Instantiate(tiles[tileIndex]);
        spawnedTiles.Add(newTile);
        float tileInterval = lastTile.transform.localScale.y / 2f + newTile.transform.localScale.y / 2f;
        newTile.transform.position = lastTile.transform.position;
        newTile.transform.position += Vector3.up * tileInterval;
        newTile.SetActive(true);
        lastTile = newTile;
        CheckForTrash();
    }
    void CheckForTrash() {
        //check from oldest until not far enough for destruction
        while (spawnedTiles[0].transform.position.y < mainCamera.transform.position.y -spawnAhead)
        {
            GameObject oldestTile = spawnedTiles[0];
            spawnedTiles.Remove(oldestTile);
            Destroy(oldestTile);
        }
    }
}
