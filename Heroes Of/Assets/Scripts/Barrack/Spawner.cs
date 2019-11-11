using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject troopPrefab;
    public Transform spawnPoint;

    private WaveManager waveManager;

    public int SpawnLimit { get; set; } = 1;
    public float SpawnFrequency { get; set; } = 1f;

    public SpawnerStates ActualState { get; private set; } = SpawnerStates.WAITING;
    private int countSpawnedEnemys = 0;
    private Coroutine spawnCicle;

    private Animator animatorController;

    //TO:DO Use RunTimeData to load and create the barrack
    private TroopFactory troopFactory;
    private RunTimeBarrackData barrackData;
    //TO:DO Store spawned troops on RunTimeData instead of the GO
    private GameObject spawnGroup;

    private void Awake() {

        waveManager = GameObject.FindGameObjectWithTag("Wave_Manager").GetComponent<WaveManager>();
        troopFactory = GameObject.FindGameObjectWithTag("Factory").GetComponent<TroopFactory>();

        animatorController = GetComponent<Animator>();
        //TO:DO use RunTimeData to get the Barrack class to this respctive GO
        barrackData = GetComponent<RunTimeBarrackData>();

        enabled = false;

    }

    private void Start() {

        if (barrackData.isEnemy) {

            spawnGroup = GameObject.FindGameObjectWithTag("Spawn_Enemy");

        }
        else {

            spawnGroup = GameObject.FindGameObjectWithTag("Spawn_Player");

        }

        waveManager.RegisterSpawnerAsListener(this);

    }

    public void StartSpawnCicle() {

        ActualState = SpawnerStates.SPAWNING;
        animatorController.SetBool("IsSpawning", true);
        spawnCicle = StartCoroutine(SpawnCicle());

    }

    public void StopSpawnCicle() {

        ActualState = SpawnerStates.WAITING;
        animatorController.SetBool("IsSpawning", false);

        if (spawnCicle != null) {

            StopCoroutine(spawnCicle);

        }
        spawnCicle = null;
        
    }

    private IEnumerator SpawnCicle() {

        for (countSpawnedEnemys = 0; countSpawnedEnemys < SpawnLimit; countSpawnedEnemys++) {

            if (!ActualState.Equals(SpawnerStates.WAITING)) {

                SpawnEnemy();
                yield return new WaitForSeconds(SpawnFrequency);

            }
            else {

                break;

            }

        }

        StopSpawnCicle();

    }

    private void SpawnEnemy() {

        Troop troop = troopFactory.CreateTroop(barrackData.codTroop, barrackData.isEnemy, barrackData.objective);
        troop.GameObject.transform.position = spawnPoint.position;
        troop.GameObject.transform.SetParent(spawnGroup.transform);

    }

}
