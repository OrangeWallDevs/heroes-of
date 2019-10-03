using UnityEngine;

public class Hero : Entity {

    private int codHero;
    private int codPart;
    private string namHero;
    private string desHero;
    private int vlrHp;
    private int vlrScore;
    private int vlrDamageDealt;
    private int vlrMotionSpeed;
    private int vlrAttackSpeed;
    private int vlrDropMoney;

    public Hero(GameObject gameObject) : base(gameObject) {

    }

    public Hero(int codHero, int codPart, string namHero, string desHero
    , int vlrHp, int vlrScore, int vlrDamageDealt, int vlrMotionSpeed
    , int vlrAttackSpeed, int vlrDropMoney) : base(null) {
        this.codHero = codHero;
        this.codPart = codPart;
        this.namHero = namHero;
        this.desHero = desHero;
        this.vlrHp = vlrHp;
        this.vlrScore = vlrScore;
        this.vlrDamageDealt = vlrDamageDealt;
        this.vlrMotionSpeed = vlrMotionSpeed;
        this.vlrAttackSpeed = vlrAttackSpeed;
        this.vlrDropMoney = vlrDropMoney;
    }

    public int CodHero1 { get => codHero; set => codHero = value; }
    public int CodPart { get => codPart; set => codPart = value; }
    public string NamHero { get => namHero; set => namHero = value; }
    public string DesHero { get => desHero; set => desHero = value; }
    public int VlrHp { get => vlrHp; set => vlrHp = value; }
    public int VlrScore { get => vlrScore; set => vlrScore = value; }
    public int VlrDamageDealt { get => vlrDamageDealt; 
    set => vlrDamageDealt = value; }
    public int VlrMotionSpeed { get => vlrMotionSpeed; 
    set => vlrMotionSpeed = value; }
    public int VlrAttackSpeed { get => vlrAttackSpeed; 
    set => vlrAttackSpeed = value; }
    public int VlrDropMoney { get => vlrDropMoney; set => vlrDropMoney = value; }
}