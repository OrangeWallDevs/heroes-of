using UnityEngine;

public class Barrack : Entity {

    private int codBarrack;
    private int codPart;
    private int codTroop;
    private string namBarrack;
    private string desBarrack;
    private float vlrSpawnFrequency;
    private int vlrCost;
    private int numTroopLimit;

    public bool isEnemy;
    public PhaseObjectives objective;

    public Barrack(GameObject gameObject) : base(gameObject) {

    }

    public Barrack(int codBarrack, int codPart, int codTroop, string namBarrack
    , string desBarrack, int vlrSpawnFrequency, int vlrCost, int numTroopLimit)
    : base(null) {
        this.codBarrack = codBarrack;
        this.codPart = codPart;
        this.codTroop = codTroop;
        this.namBarrack = namBarrack;
        this.desBarrack = desBarrack;
        this.vlrSpawnFrequency = vlrSpawnFrequency;
        this.vlrCost = vlrCost;
        this.numTroopLimit = numTroopLimit;
    }

    public int CodBarrack { get => codBarrack; set => codBarrack = value; }
    public int CodPart { get => codPart; set => codPart = value; }
    public int CodTroop { get => codTroop; set => codTroop = value; }
    public string NamBarrack { get => namBarrack; set => namBarrack = value; }
    public string DesBarrack { get => desBarrack; set => desBarrack = value; }
    public float VlrSpawnFrequency {
        get => vlrSpawnFrequency;
        set {

            vlrSpawnFrequency = value;

            if (GameObject != null) {

                Spawner spawner = GameObject.GetComponent<Spawner>();
                spawner.SpawnFrequency = vlrSpawnFrequency;

            }

        }
    }
    public int VlrCost { get => vlrCost; set => vlrCost = value; }
    public int NumTroopLimit {
        get => numTroopLimit;
        set {

            numTroopLimit = value;

            if (GameObject != null) {

                Spawner spawner = GameObject.GetComponent<Spawner>();
                spawner.SpawnLimit = numTroopLimit;

            }

        }
    }

    public bool IsEnemy { get => isEnemy; set => isEnemy = value; }

    public PhaseObjectives Objective { get => objective; set => objective = value; }

}