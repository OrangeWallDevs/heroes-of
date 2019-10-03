using UnityEngine;

public class Troop : Entity {

    private int codTroop;
    private string namTroop;
    private int vlrDamageDealt;
    private int vlrHp;
    private int vlrScore;
    private int vlrMotionSpeed;
    private int vlrAttackSpeed;
    private int vlrDropMoney;

    public Troop(GameObject gameObject) : base(gameObject) {

    }

    public Troop(int codTroop, string namTroop, int vlrDamageDealt, int vlrHp
    , int vlrScore, int vlrMotionSpeed, int vlrAttackSpeed, int vlrDropMoney)
    : base(null) {
        this.codTroop = codTroop;
        this.namTroop = namTroop;
        this.vlrDamageDealt = vlrDamageDealt;
        this.vlrHp = vlrHp;
        this.vlrScore = vlrScore;
        this.vlrMotionSpeed = vlrMotionSpeed;
        this.vlrAttackSpeed = vlrAttackSpeed;
        this.vlrDropMoney = vlrDropMoney;
    }

    public int CodTroop { get => codTroop; set => codTroop = value; }
    public string NamTroop { get => namTroop; set => namTroop = value; }
    public int VlrDamageDealt { get => vlrDamageDealt; 
    set => vlrDamageDealt = value; }
    public int VlrHp { get => vlrHp; set => vlrHp = value; }
    public int VlrScore { get => vlrScore; set => vlrScore = value; }
    public int VlrMotionSpeed { get => vlrMotionSpeed;
     set => vlrMotionSpeed = value; }
    public int VlrAttackSpeed { get => vlrAttackSpeed; 
    set => vlrAttackSpeed = value; }
    public int VlrDropMoney { get => vlrDropMoney; set => vlrDropMoney = value; }
}