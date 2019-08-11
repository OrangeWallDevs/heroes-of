using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public float timeCountDown = 5f;
    public float countDown = 0f;
    public int maxWaves = 1;

    public WavePanelManager wavesCounterPanel;

    private int actualWave = 0;
    private WaveStates actualState = WaveStates.COUNTING;

    private float winConditionCheckCount = 1f;
    private List<Spawner> spawners;

    private void Start() {

        spawners = new List<Spawner>();

        countDown = timeCountDown;

    }

    private void Update() {

        if (actualState == WaveStates.RUNNING) {

            if (!StillHaveEnemys())
                CompleteWave();

            return;

        }

        if (spawners.Count > 0) {

            if (countDown <= 0 && !actualState.Equals(WaveStates.RUNNING)) {
                StartWave();
            } 
            else {
                actualState = WaveStates.COUNTING;
                countDown -= Time.deltaTime;
            }

        }

    }

    private void StartWave() {

        actualState = WaveStates.RUNNING;
        actualWave++;

        foreach (Spawner spawnPoint in spawners) {

            spawnPoint.ActualState = SpawnerStates.SPAWNING;

            StartCoroutine(spawnPoint.SpawnCicle());

        }

    }

    private void CompleteWave() {

        foreach (Spawner spawnPoint in spawners) {
            spawnPoint.ActualState = SpawnerStates.WAITING;
        }

        if (actualWave >= maxWaves) {
            Debug.Log("Level completed");
            return;
        }

        wavesCounterPanel.UpdateCounters();

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

    public int ActualWave { get { return actualWave; } }

    public void AddSpawner(Spawner spawner) {

        spawners.Add(spawner);

        spawner.ActualState = SpawnerStates.SPAWNING;
        StartCoroutine(spawner.SpawnCicle());

    }

    public void RemoveSpawner(Spawner spawner) {

        spawner.ActualState = SpawnerStates.WAITING;
        StopCoroutine(spawner.SpawnCicle());

        spawners.Remove(spawner);

    }

    public void ClearSpawners() {

        spawners.Clear();

    }

}
