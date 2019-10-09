using UnityEngine;

public class Tower : Entity {

    private int codTower;
    private int valHp;
    private int numEffectArea;

    public Tower(GameObject gameObject) : base(gameObject) {

    }

    public Tower(int codTower, int valHp, int numEffectArea) : base(null) {
        this.codTower = codTower;
        this.valHp = valHp;
        this.numEffectArea = numEffectArea;
    }

    public int CodTower { get => codTower; set => codTower = value; }
    public int ValHp { get => valHp; set => valHp = value; }
    public int NumEffectArea { get => numEffectArea; set => numEffectArea = value; }
}