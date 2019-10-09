using UnityEngine;

public class Troop : Entity {

    public int CodTroop { get; set; }
    public string NamTroop { get; set; }
    public int ValDamageDealt { get; set; }
    public int ValHp { get; set; }
    public int ValScore { get; set; }
    public float ValMotionSpeed { get; set; }
    public float ValAttackSpeed { get; set; }
    public int ValDropMoney { get; set; }

    public float ValAttackDistance { get; set; }
    public PhaseObjectives Objective { get; set; }
    public bool AttackAtDistance { get; set; }
    public bool IsEnemy { get; set; }

    public Troop(GameObject gameObject) : base(gameObject) {

    }

    public Troop(int codTroop, string namTroop, int valDamageDealt, int valHp, 
      int valScore, float valMotionSpeed, float valAttackSpeed, float valAttackDistance,
      int valDropMoney, PhaseObjectives troopObjective, bool attackAtDistance, bool isEnemy)
    : base(null) {

        CodTroop = codTroop;
        NamTroop = namTroop;
        ValDamageDealt = valDamageDealt;
        ValHp = valHp;
        ValScore = valScore;
        ValMotionSpeed = valMotionSpeed;
        ValAttackSpeed = valAttackSpeed;
        ValDropMoney = valDropMoney;
        ValAttackDistance = valAttackDistance;
        Objective = troopObjective;
        AttackAtDistance = attackAtDistance;
        IsEnemy = isEnemy;

    }

}