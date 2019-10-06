using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject troopPrefab;
    public Transform spawnPoint;

    private WaveManager waveManager;

    public int SpawnLimit { get; set; } = 1;
    public float SpawnFrequency { get; set; } = 1f;

    private int countSpawnedEnemys = 0;
    private SpawnerStates actualState = SpawnerStates.WAITING;
    private Coroutine spawnCicle;

    private Animator animatorController;

    //TO:DO Use RunTimeData to load and create the barrack
    private TroopFactory troopFactory;
    private RunTimeBarrackData barrackData;

    private void Awake() {

        waveManager = GameObject.FindGameObjectWithTag("Wave_Manager").GetComponent<WaveManager>();
        troopFactory = GameObject.FindGameObjectWithTag("Factory").GetComponent<TroopFactory>();

        animatorController = GetComponent<Animator>();
        //TO:DO use RunTimeData to get the Barrack class to this respctive GO
        barrackData = GetComponent<RunTimeBarrackData>();

    }

    private void Start() {

        waveManager.RegisterSpawnerAsListener(this);

    }

    public void StartSpawnCicle() {

        actualState = SpawnerStates.SPAWNING;
        animatorController.SetBool("IsSpawning", true);
        spawnCicle = StartCoroutine(SpawnCicle());

    }

    public void StopSpawnCicle() {

        actualState = SpawnerStates.WAITING;
        animatorController.SetBool("IsSpawning", false);
        StopCoroutine(spawnCicle);
        spawnCicle = null;
        
    }

    private IEnumerator SpawnCicle() {

        for (countSpawnedEnemys = 1; countSpawnedEnemys < SpawnLimit; countSpawnedEnemys++) {

            if (!actualState.Equals(SpawnerStates.WAITING)) {

                SpawnEnemy();
                yield return new WaitForSeconds(SpawnFrequency);

            }
            else {

                break;

            }

        }

    }

    private void SpawnEnemy() {

        Troop troop = troopFactory.CreateTroop(barrackData.codTroop, barrackData.isEnemy, barrackData.objective);
        troop.GameObject.transform.position = spawnPoint.position;

    }

}
