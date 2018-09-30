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
    public SpriteRenderer background;
    List<GameObject> backgrounds;
    public LootBoxController lootBox;
    GameObject lastBackground;
    public float lootBoxRate; // How many loot boxes per block?
    public GameObject[] deadStudios;
    public float deadStudioChance = 0.5f;
	// Use this for initialization
	void Awake () {
        backgrounds = new List<GameObject>();
        //backgrounds.Add(background.gameObject);
        lastBackground = background.gameObject;

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
        if (lastBackground.transform.position.y - mainCamera.transform.position.y < spawnAhead)
        {
            SpawnBackGround();
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
        SpawnLootBox();
        SpawnDeadStudio();
    }
    void CheckForTrash() {
        //check from oldest until not far enough for destruction
        while (spawnedTiles[0].transform.position.y < mainCamera.transform.position.y -spawnAhead)
        {
            GameObject oldestTile = spawnedTiles[0];
            spawnedTiles.Remove(oldestTile);
            Destroy(oldestTile);
        }
        while (backgrounds.Count > 0 && backgrounds[0].transform.position.y < mainCamera.transform.position.y - spawnAhead)
        {
            GameObject oldestBackground = backgrounds[0];
            backgrounds.Remove(oldestBackground);
            Destroy(oldestBackground);
        }
    }
    void SpawnBackGround() {
        GameObject newBackGround = Instantiate(background.gameObject);
        newBackGround.transform.position = lastBackground.transform.position + background.bounds.size.y * Vector3.up;
        backgrounds.Add(newBackGround);
        lastBackground = newBackGround;
    }
    void SpawnLootBox() {
        float tileWidth = lastTile.transform.localScale.x / 2f;
        int maxTries = 20;

        for (var j = 0; j < lootBoxRate; ++j)
        {
            for (int i = 0; i < maxTries; i++)
            {
                Vector3 rayStartPos = lastTile.transform.position + lastTile.transform.localScale.y / 2 * Vector3.up + Random.Range(-tileWidth, tileWidth) * Vector3.right;
                RaycastHit2D ray = Physics2D.Raycast(rayStartPos, Vector2.down, spawnAhead);
                if (ray.collider != null && ray.collider.tag == "ground")
                {
                    GameObject newBox = Instantiate(lootBox.gameObject, ray.point + 0.4f * Vector2.up, Quaternion.identity);
                    newBox.transform.parent = lastTile.transform;
                    break;
                }
            }
        }
        
    }
    void SpawnDeadStudio() {
        if (Random.Range(0f,1f) <= deadStudioChance)
        {
            int deadIndex = Random.Range(0,deadStudios.Length);
            GameObject newDead = Instantiate(deadStudios[deadIndex], lastTile.transform.position, Quaternion.identity);
            float tileWidth = lastTile.transform.localScale.x / 2;
            newDead.transform.position += Vector3.right * Random.Range(-tileWidth,tileWidth);
            Rigidbody2D rb = newDead.GetComponent<Rigidbody2D>();
            float maxSpin = 500f;
            rb.angularVelocity = Random.Range(-maxSpin,maxSpin);
            rb.gravityScale = 0.2f;
            //Vector2 randomVelociy = Vector2.right * Random.Range(-2f,2f);
            //rb.velocity = randomVelociy;
            rb.drag = .2f;
        }
    }
}
