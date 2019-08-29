﻿using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;

    private int spawnLimit = 1;
    private float spawnFrequency = 1f;

    private int countSpawnedEnemys = 0;
    private SpawnerStates actualState = SpawnerStates.WAITING;

    private Transform spawnPoint;

    private void Awake() {
        
        spawnPoint = transform.Find("Spawn_Point");

    }

    private void Start() {

        WaveManager waveManager = WaveManager.Instance;
        waveManager.RegisterSpawnerAsListener(this);

    }

    public void StartSpawnCicle() {

        actualState = SpawnerStates.SPAWNING;

        StartCoroutine(SpawnCicle());

    }

    public void StopSpawnCicle() {

        actualState = SpawnerStates.WAITING;

        StopCoroutine(SpawnCicle());
        
    }

    private IEnumerator SpawnCicle() {

        for (countSpawnedEnemys = 1; countSpawnedEnemys < spawnLimit; countSpawnedEnemys++) {

            if (!actualState.Equals(SpawnerStates.WAITING)) {

                SpawnEnemy();
                yield return new WaitForSeconds(spawnFrequency);

            }
            else {

                break;

            }

        }

        StopSpawnCicle();

    }

    private void SpawnEnemy() {

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

    }

    public int SpawnLimit {

        get { return spawnLimit; }

        set { spawnLimit = value; }

    }

    public float SpawnFrequency {

        get { return spawnFrequency; }

        set { spawnFrequency = value; }

    }

}
