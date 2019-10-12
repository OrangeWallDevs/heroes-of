using UnityEngine;

public class Tower : Entity {

    private int codTower;
    private int valHp;
    private int numEffectArea;
    private bool idtIsEnemy;

    public int CodTower { get => codTower; set => codTower = value; }
    public int ValHp { get => valHp; set => valHp = value; }
    public int NumEffectArea { get => numEffectArea; set => numEffectArea = value; }
    public bool IdtIsEnemy { get => idtIsEnemy; set => idtIsEnemy = value; }

    // Runtime members:
    public GameObject GameObject { get; set; }

    public Tower() {
    }

    public Tower(int codTower, int valHp, int numEffectArea) {
        this.codTower = codTower;
        this.valHp = valHp;
        this.numEffectArea = numEffectArea;
    }
}