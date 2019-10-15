using UnityEngine;

public class Barrack : Entity, IGamePrototype {

    private int codBarrack;
    private int codPart;
    private int codTroop;
    private string namBarrack;
    private string desBarrack;
    private float valSpawnFrequency;
    private int valCost;
    private int numTroopLimit;

    public int CodBarrack { get => codBarrack; set => codBarrack = value; }
    public int CodPart { get => codPart; set => codPart = value; }
    public int CodTroop { get => codTroop; set => codTroop = value; }
    public string NamBarrack { get => namBarrack; set => namBarrack = value; }
    public string DesBarrack { get => desBarrack; set => desBarrack = value; }
    public float ValSpawnFrequency { get => valSpawnFrequency; set => valSpawnFrequency = value; }
    public int ValCost { get => valCost; set => valCost = value; }
    public int NumTroopLimit { get => numTroopLimit; set => numTroopLimit = value; }
    
    // Runtime members:
    public bool isEnemy;
    public PhaseObjectives objective;

    public GameObject GameObject { get; set; }
    public Part Part { get; set; }
    public Troop Troop { get; set; }
    public bool IsEnemy { get => isEnemy; set => isEnemy = value; }
    public PhaseObjectives Objective { get => objective; set => objective = value; }

    public Barrack() {
    }

    public Barrack(int codBarrack, int codPart, int codTroop, string namBarrack,
            string desBarrack, int valSpawnFrequency, int valCost, int numTroopLimit) {
        this.codBarrack = codBarrack;
        this.codPart = codPart;
        this.codTroop = codTroop;
        this.namBarrack = namBarrack;
        this.desBarrack = desBarrack;
        this.valSpawnFrequency = valSpawnFrequency;
        this.valCost = valCost;
        this.numTroopLimit = numTroopLimit;
    }

    public void SetUpSpawner() {
        if (GameObject != null) {
            Spawner spawner = GameObject.GetComponent<Spawner>();

            spawner.SpawnFrequency = valSpawnFrequency;
            spawner.SpawnLimit = numTroopLimit;            
        }
    }

}