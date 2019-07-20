using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform typeEnemy;
    public int spawnLimit = 15;
    public float spawnFrequency = 1f;

    // Public apenas para Debug
    public int countSpawnedEnemys = 0;
    public SpawnerStates actualState = SpawnerStates.WAITING;

    // Essa função apenas spawnad e seta o statos do spawner de acordo com o limimte
    // Ela não controla os turnos
    public IEnumerator SpawnCicle() {

        // Se o spawner atinge seu limite ele para
        if (countSpawnedEnemys >= spawnLimit || actualState == SpawnerStates.WAITING) {

            actualState = SpawnerStates.WAITING;

            Debug.Log("Limite spawn atingido estado de espera");

            yield break;

        }

        for (int i = 0; i < spawnLimit; i++) {

            SpawnEnemy();

            // Faz a cazerna esperar para spawnar o próximo
            yield return new WaitForSeconds(spawnFrequency);

        }

        yield break;
    }

    private void SpawnEnemy() {

        countSpawnedEnemys++;

        Debug.Log("Spawn enemy: " + countSpawnedEnemys);

        Instantiate(typeEnemy, transform.position, transform.rotation);

    }
}
