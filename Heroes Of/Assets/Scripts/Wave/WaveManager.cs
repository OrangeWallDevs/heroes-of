using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : Singleton<WaveManager> {

    public float timeCountDown = 5f;
    public float countDown = 0f;
    public int maxWaves = 1;

    private int registeredSpawners = 0;
    public GameEvent newWaveStart, waveEnd;

    private int actualWave = 0;
    private WaveStates actualState = WaveStates.WAITING;

    private float winConditionCheckCount = 1f;

    private void Awake() {

        newWaveStart = ScriptableObject.CreateInstance<GameEvent>();
        waveEnd = ScriptableObject.CreateInstance<GameEvent>();

        countDown = timeCountDown;

    }

    private void Update() {

        if (actualState == WaveStates.RUNNING) {

            if (!StillHaveEnemys()) {

                CompleteWave();

            }

            return;

        }

        if (registeredSpawners > 0) {

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

        newWaveStart.Raise();

    }

    private void CompleteWave() {

        waveEnd.Raise();

        if (actualWave >= maxWaves) {
            Debug.Log("Level completed");
            return;
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

    public int ActualWave { get { return actualWave; } }

    public void RegisterSpawner(UnityAction waveStartAction, UnityAction waveEndAction) {

        newWaveStart.RegisterListener(waveStartAction);
        waveEnd.RegisterListener(waveEndAction);
        registeredSpawners++;

        if (actualState == WaveStates.RUNNING) {

            waveStartAction();

        }

    }

    public void UnregisterSpawner(UnityAction waveStartAction, UnityAction waveEndAction) {

        newWaveStart.UnregisterListener(waveStartAction);
        waveEnd.UnregisterListener(waveEndAction);
        registeredSpawners--;

    }

    /*public void AddSpawner(Spawner spawner) {

        spawners.Add(spawner);

        if (actualState == WaveStates.RUNNING) {

            spawner.StartSpawnCicle();

        } 

    }

    public void RemoveSpawner(Spawner spawner) {

        spawner.StopSpawnCicle();

        spawners.Remove(spawner);

    }

    public void ClearSpawners() {

        spawners.Clear();

    }*/

}
