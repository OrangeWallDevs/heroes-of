using UnityEngine;

public class Troop : Entity {

    int codTroop;
    string namTroop;
    string txtAssetIdentifier;
    int valDamageDealt;
    int valHp;
    int valScore;
    float valMotionSpeed;
    float valAttackSpeed;
    int valDropMoney;

    public int CodTroop { get => codTroop; set => codTroop = value; }
    public string NamTroop { get => namTroop; set => namTroop = value; }
    public string TxtAssetIdentifier { get => txtAssetIdentifier; set => txtAssetIdentifier = value; }
    public int ValDamageDealt { get => valDamageDealt; set => valDamageDealt = value; }
    public int ValHp { get => valHp; set => valHp = value; }
    public int ValScore { get => valScore; set => valScore = value; }
    public float ValMotionSpeed { get => valMotionSpeed; set => valMotionSpeed = value; }
    public float ValAttackSpeed { get => valAttackSpeed; set => valAttackSpeed = value; }
    public int ValDropMoney { get => valDropMoney; set => valDropMoney = value; }    

    public float ValAttackDistance { get; set; }
    public PhaseObjectives Objective { get; set; }
    public bool AttackAtDistance { get; set; }
    public bool IsEnemy { get; set; }

    public Troop(GameObject gameObject) : base(gameObject) {

    }

}