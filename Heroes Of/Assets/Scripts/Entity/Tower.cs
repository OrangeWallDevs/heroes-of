using UnityEngine;

public class Tower : Entity {

    private int codTower;
    private int vlrHp;
    private int numEffectArea;

    public Tower(GameObject gameObject) : base(gameObject) {

    }

    public Tower(int codTower, int vlrHp, int numEffectArea) : base(null) {
        this.codTower = codTower;
        this.vlrHp = vlrHp;
        this.numEffectArea = numEffectArea;
    }

    public int CodTower { get => codTower; set => codTower = value; }
    public int VlrHp { get => vlrHp; set => vlrHp = value; }
    public int NumEffectArea { get => numEffectArea; set => numEffectArea = value; }
}