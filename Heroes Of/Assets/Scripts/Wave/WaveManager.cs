using UnityEngine;

public class WaveManager : Singleton<WaveManager> {

    public float startTimeCountDown = 5f;
    public int maxWaves = 1;

    public GameEvent waveStartEvent, waveEndEvent, finalWaveCompleted;

    private int actualWave = 0;
    private WaveStates actualState = WaveStates.WAITING;

    private float winConditionCheckCount = 1f, countDown = 0f;

    private void Awake() {

        countDown = startTimeCountDown;

    }

    private void Update() {

        if (actualState == WaveStates.RUNNING) {

            if (!StillHaveEnemys()) {

                CompleteWave();

            }

            return;

        }

        if (waveStartEvent.ListenersCount > 0) {

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

        waveStartEvent.Raise();

    }

    private void CompleteWave() {

        waveEndEvent.Raise();

        if (actualWave >= maxWaves) {

            finalWaveCompleted.Raise();
            return;

        }

        countDown = startTimeCountDown;
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

    public void RegisterSpawnerAsListener(Spawner spawner) {

        waveStartEvent.RegisterListener(spawner.StartSpawnCicle);
        waveEndEvent.RegisterListener(spawner.StopSpawnCicle);

        if (actualState.Equals(WaveStates.RUNNING)) {

            spawner.StartSpawnCicle();

        }

    }

    public void UnregisterSpawnerAsListener(Spawner spawner) {

        waveStartEvent.UnregisterListener(spawner.StartSpawnCicle);
        waveEndEvent.UnregisterListener(spawner.StopSpawnCicle);

        if (actualState.Equals(WaveStates.RUNNING)) {

            spawner.StopSpawnCicle();

        }

    }

    public float CountDown { get { return countDown; } }

    public int ActualWave { get { return actualWave; } }

    public WaveStates ActualState { get { return actualState; } }

}
