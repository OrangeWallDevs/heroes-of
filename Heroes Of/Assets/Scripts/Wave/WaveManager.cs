using UnityEngine;

public class WaveManager : MonoBehaviour {

    public float timeCountDown = 5f;
    public float countDown = 0f;
    public int maxWaves = 1;

    private int actualWave = 0;
    private WaveStates actualState = WaveStates.COUNTING;

    private float winConditionCheckCount = 1f;
    private Spawner[] spawners;

    private void Start() {

        GameObject[] spawnersObjetcs = GameObject.FindGameObjectsWithTag("Spawner");
        spawners = new Spawner[spawnersObjetcs.Length];

        for (int i = 0; i < spawnersObjetcs.Length; i++) {
            spawners[i] = spawnersObjetcs[i].GetComponent<Spawner>();
        }

        countDown = timeCountDown;

    }

    private void Update() {

        if (actualState == WaveStates.RUNNING) {

            if (!StillHaveEnemys()) 
                CompleteWave();

            return;

        }

        if (countDown <= 0 && !actualState.Equals(WaveStates.RUNNING)) {
            StartWave();
        } 
        else {
            actualState = WaveStates.COUNTING;
            countDown -= Time.deltaTime;
        }

    }

    private void StartWave() {

        actualState = WaveStates.RUNNING;
        actualWave++;

        foreach (Spawner spawnPoint in spawners) {

            spawnPoint.countSpawnedEnemys = 0;
            spawnPoint.actualState = SpawnerStates.SPAWNING;

            StartCoroutine(spawnPoint.SpawnCicle());

        }

    }

    private void CompleteWave() {

        if (actualWave >= maxWaves) {
            Debug.Log("Nível Completo. Restart ... ");
        }

        countDown = timeCountDown;
        actualState = WaveStates.COUNTING;

    }

    private bool StillHaveEnemys() {

        winConditionCheckCount -= Time.deltaTime;

        if (winConditionCheckCount >= 0) {
            return true;
        }

        winConditionCheckCount = 1f;
        bool hasEnemys = (GameObject.FindGameObjectWithTag("Enemy") != null);

        return hasEnemys;

    }

    public int MaxWaves { get { return maxWaves; } }

    public int ActualWave { get { return actualWave; } }

}
