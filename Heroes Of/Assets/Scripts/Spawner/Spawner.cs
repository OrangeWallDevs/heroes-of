using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;

    public int spawnLimit = 1;
    public float spawnFrequency = 1f;

    private int countSpawnedEnemys = 0;
    private SpawnerStates actualState = SpawnerStates.WAITING;

    private Transform spawnPoint;

    private void Awake() {
        
        spawnPoint = transform.Find("Spawn_Point");

    }

    public IEnumerator SpawnCicle() {

        for (countSpawnedEnemys = 1; countSpawnedEnemys < spawnLimit; countSpawnedEnemys++) {

            if (!actualState.Equals(SpawnerStates.WAITING)) {

                SpawnEnemy();
                yield return new WaitForSeconds(spawnFrequency);

            }
            else {

                break;

            }

        }

        actualState = SpawnerStates.WAITING;

    }

    private void SpawnEnemy() {

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

    }

    public int CountSpawnedEnemys {

        get { return countSpawnedEnemys; }

        set { countSpawnedEnemys = value; }

    }

    public SpawnerStates ActualState {

        get { return actualState; }

        set { actualState = value; }

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
