using UnityEngine;

public class WaveManager : MonoBehaviour {

    public float startTimeCountDown = 5f;
    public int maxWaves = 1;

    public GameEvent waveStartEvent, waveEndEvent, finalWaveStartEvent, finalWaveEndEvent;
    public TowerEvent towerDestroyedEvent;

    public int ActualWave { get; private set; } = 0;
    public WaveStates ActualState { get; private set; } = WaveStates.WAITING;

    private float winConditionCheckCount = 1f, countDown = 0f;

    private void Awake() {

        countDown = startTimeCountDown;

    }

    private void Start() {

        waveEndEvent.RegisterListener(HandleWaveEnd);
        towerDestroyedEvent.RegisterListener(HandleTowerDestroyed);
        
    }

    private void Update() {

        if (ActualState != WaveStates.RUNNING) {

            if (waveStartEvent.ListenersCount > 0) {

                if (countDown <= 0) {

                    StartWave();

                } 
                else {

                    ActualState = WaveStates.COUNTING;
                    countDown -= Time.deltaTime;

                }

            }

        }

    }

    private void StartWave() {

        ActualState = WaveStates.RUNNING;
        ActualWave++;

        if (ActualWave >= maxWaves) {

            finalWaveStartEvent.Raise();

        } 
        else {

            waveStartEvent.Raise();

        }


    }

    private void CompleteWave() {

        if (ActualWave >= maxWaves) {

            finalWaveEndEvent.Raise();

        }
        else {

            waveEndEvent.Raise();

        }

    }

    private void HandleTowerDestroyed(RunTimeTowerData tower) {

        CompleteWave();

    }

    private void HandleWaveEnd() {

        countDown = startTimeCountDown;
        ActualState = WaveStates.COUNTING;

        //GameObject[] troops = GameObject.FindGameObjectsWithTag("Troop");

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
        finalWaveStartEvent.RegisterListener(spawner.StartSpawnCicle);
        finalWaveEndEvent.RegisterListener(spawner.StopSpawnCicle);

        if (ActualState.Equals(WaveStates.RUNNING)) {

            spawner.StartSpawnCicle();

        }

    }

    public void UnregisterSpawnerAsListener(Spawner spawner) {

        waveStartEvent.UnregisterListener(spawner.StartSpawnCicle);
        waveEndEvent.UnregisterListener(spawner.StopSpawnCicle);
        finalWaveStartEvent.UnregisterListener(spawner.StartSpawnCicle);
        finalWaveEndEvent.UnregisterListener(spawner.StopSpawnCicle);

        if (ActualState.Equals(WaveStates.RUNNING)) {

            spawner.StopSpawnCicle();

        }

    }

    public float CountDown { get { return countDown; } }

}
