using UnityEngine;

public class WaveManager : MonoBehaviour {

    public float startTimeCountDown = 5f;
    public int maxWaves = 1;

    public int ActualWave { get; private set; } = 0;
    public WaveStates ActualState { get; private set; } = WaveStates.WAITING;

    public GameEvent waveStartEvent, waveEndEvent, finalWaveCompleted;
    private float winConditionCheckCount = 1f, countDown = 0f;

    private void Awake() {

        countDown = startTimeCountDown;

    }

    private void Update() {

        if (ActualState == WaveStates.RUNNING) {

            if (!StillHaveEnemys()) {

                CompleteWave();

            }

            return;

        }

        if (waveStartEvent.ListenersCount > 0) {

            if (countDown <= 0 && !ActualState.Equals(WaveStates.RUNNING)) {

                StartWave();

            } 
            else {

                ActualState = WaveStates.COUNTING;
                countDown -= Time.deltaTime;

            }

        }

    }

    private void StartWave() {

        ActualState = WaveStates.RUNNING;
        ActualWave++;

        waveStartEvent.Raise();

    }

    private void CompleteWave() {

        waveEndEvent.Raise();

        if (ActualWave >= maxWaves) {

            finalWaveCompleted.Raise();
            return;

        }

        countDown = startTimeCountDown;
        ActualState = WaveStates.COUNTING;

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

        if (ActualState.Equals(WaveStates.RUNNING)) {

            spawner.StartSpawnCicle();

        }

    }

    public void UnregisterSpawnerAsListener(Spawner spawner) {

        waveStartEvent.UnregisterListener(spawner.StartSpawnCicle);
        waveEndEvent.UnregisterListener(spawner.StopSpawnCicle);

        if (ActualState.Equals(WaveStates.RUNNING)) {

            spawner.StopSpawnCicle();

        }

    }

    public float CountDown { get { return countDown; } }

}
