using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public int spawnLimit = 1;
    public float spawnFrequency = 1f;

    private int countSpawnedEnemys = 0;
    private SpawnerStates actualState = SpawnerStates.WAITING;

    private Transform spawnPoint;

    private void Start() {
        
        spawnPoint = transform.Find("Spawn_Point");

    }

    public IEnumerator SpawnCicle() {

        for (countSpawnedEnemys = 0; countSpawnedEnemys < spawnLimit; countSpawnedEnemys++) {

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

        set { countSpawnedEnemys = value; }

        get { return countSpawnedEnemys; }

    }

    public SpawnerStates ActualState {

        set { actualState = value; }

        get { return actualState; }

    }
}
