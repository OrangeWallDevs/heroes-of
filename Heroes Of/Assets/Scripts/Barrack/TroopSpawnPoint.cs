using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopSpawnPoint : MonoBehaviour {

    private GameObject spawnPoint;
    private TilemapHandler tilemapHandler;

    void Start () {
        tilemapHandler = GameObject.Find("TilemapHandler").GetComponent<TilemapHandler>();
        spawnPoint = this.gameObject.transform.GetChild(0).gameObject;
        Vector2 closestWalkablePosition = tilemapHandler.GetClosestWalkableTile(spawnPoint.transform.position);
        spawnPoint.transform.position = closestWalkablePosition;
    }
}