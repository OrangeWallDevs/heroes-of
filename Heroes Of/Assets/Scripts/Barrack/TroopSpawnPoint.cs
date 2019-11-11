using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopSpawnPoint : MonoBehaviour {

    private GameObject spawnPoint;
    private TilemapHandler tilemapHandler;
    private Spawner spawner;

    private void Awake() {
        tilemapHandler = GameObject.Find("TilemapHandler").GetComponent<TilemapHandler>();
        spawner = GetComponent<Spawner>();
    }

    void Start () {
        spawnPoint = this.gameObject.transform.GetChild(0).gameObject;
        Vector2 closestWalkablePosition = tilemapHandler.GetClosestWalkableTile(spawnPoint.transform.position);
        spawnPoint.transform.position = closestWalkablePosition;

        spawner.enabled = true;
    }
}