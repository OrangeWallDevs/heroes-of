using UnityEngine;

public class Troop : Entity, IGamePrototype {

    int codTroop;
    string namTroop;
    string txtAssetIdentifier;
    int valDamageDealt;
    int valHp;
    int valScore;
    float valMotionSpeed;
    float valAttackSpeed;
    int valDropMoney;
    float valAttackDistance;
    bool idtAttackAtDistance;

    public int CodTroop { get => codTroop; set => codTroop = value; }
    public string NamTroop { get => namTroop; set => namTroop = value; }
    public string TxtAssetIdentifier { get => txtAssetIdentifier; set => txtAssetIdentifier = value; }
    public int ValDamageDealt { get => valDamageDealt; set => valDamageDealt = value; }
    public int ValHp { get => valHp; set => valHp = value; }
    public int ValScore { get => valScore; set => valScore = value; }
    public float ValMotionSpeed { get => valMotionSpeed; set => valMotionSpeed = value; }
    public float ValAttackSpeed { get => valAttackSpeed; set => valAttackSpeed = value; }
    public int ValDropMoney { get => valDropMoney; set => valDropMoney = value; }    
    public float ValAttackDistance { get => valAttackDistance; set => valAttackDistance = value; }
    public bool IdtAttackAtDistance { get => idtAttackAtDistance; set => idtAttackAtDistance = value; }

    // Runtime members:
    public GameObject GameObject { get; set; }
    public PhaseObjectives Objective { get; set; }
    public bool IsEnemy { get; set; }

    public Troop() {
    }

}