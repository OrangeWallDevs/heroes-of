using UnityEngine;

public class WaveManager : MonoBehaviour {

    public float startTimeCountDown = 5f;
    public int maxWaves = 1;

    public GameEvent waveStartEvent, waveEndEvent;
    public GameEvent finalWaveStartEvent, finalWaveEndEvent;
    public GameEvent startCountDownEvent;
    public GameEvent allTroopsRemoved;
    public TowerEvent towerDestroyedEvent;

    public int ActualWave { get; private set; } = 0;
    public WaveStates ActualState { get; private set; } = WaveStates.WAITING;

    private float countDown = 0f;

    private GameObject playerSpawnGroup, enemySpawnGroup;
    private bool isTurnTrasition = false;

    private void Awake() {

        countDown = startTimeCountDown;
        enemySpawnGroup = GameObject.FindGameObjectWithTag("Spawn_Enemy");
        playerSpawnGroup = GameObject.FindGameObjectWithTag("Spawn_Player");

    }

    private void Start() {

        waveEndEvent.RegisterListener(HandleWaveEnd);
        allTroopsRemoved.RegisterListener(HandleAllTroopsRemoved);
        towerDestroyedEvent.RegisterListener(HandleTowerDestroyed);
        
    }

    private void Update() {

        if (ActualState != WaveStates.RUNNING) {

            if (waveStartEvent.ListenersCount > 1) {

                if (countDown <= 0) {

                    StartWave();

                } 
                else {

                    ActualState = WaveStates.COUNTING;
                    countDown -= Time.deltaTime;

                    if (countDown <= 10) {

                        startCountDownEvent.Raise();

                    }

                }

            }

        }

    }

    private void StartWave() {

        ActualState = WaveStates.RUNNING;
        isTurnTrasition = false;
        ActualWave++;

        if (ActualWave >= maxWaves) {

            finalWaveStartEvent.Raise();

        } 
        else {

            waveStartEvent.Raise();

        }


    }

    private void CompleteWave() {

        isTurnTrasition = true;

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

    }

    private void HandleAllTroopsRemoved() {

        GameObject spawnGroup = null;
        bool checkIsEnemy = false;

        if (isTurnTrasition) {

            return;

        }

        if (playerSpawnGroup.transform.childCount == 0) {

            spawnGroup = playerSpawnGroup;
            checkIsEnemy = false;

        }
        else if (enemySpawnGroup.transform.childCount == 0) {

            spawnGroup = enemySpawnGroup;
            checkIsEnemy = true;

        }

        if (spawnGroup != null) {

            // TO:DO --> Use list of barracks from RunTimeData [This part of code is to heavy]
            GameObject[] barracks = GameObject.FindGameObjectsWithTag("Spawner");
            int activeBarracks = 0;

            foreach (GameObject barrack in barracks) {

                RunTimeBarrackData barrackData = barrack.GetComponent<RunTimeBarrackData>();
                Spawner barrackSpawner = barrack.GetComponent<Spawner>();

                if (barrackData.isEnemy == checkIsEnemy && barrackSpawner.ActualState == SpawnerStates.SPAWNING) {

                    activeBarracks++;

                }

            }

            if (activeBarracks == 0) {

                CompleteWave();

            }

        }

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
