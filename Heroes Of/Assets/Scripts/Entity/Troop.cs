using UnityEngine;

public class Troop : Entity {

    public int CodTroop { get; set; }
    public string NamTroop { get; set; }
    public int VlrDamageDealt { get; set; }
    public int VlrHp { get; set; }
    public int VlrScore { get; set; }
    public float VlrMotionSpeed { get; set; }
    public float VlrAttackSpeed { get; set; }
    public int VlrDropMoney { get; set; }

    public float VlrAttackDistance { get; set; }
    public PhaseObjectives Objective { get; set; }
    public bool AttackAtDistance { get; set; }
    public bool IsEnemy { get; set; }

    public Troop(GameObject gameObject) : base(gameObject) {

    }

    public Troop(int codTroop, string namTroop, int vlrDamageDealt, int vlrHp, 
      int vlrScore, float vlrMotionSpeed, float vlrAttackSpeed, float vlrAttackDistance,
      int vlrDropMoney, PhaseObjectives troopObjective, bool attackAtDistance, bool isEnemy)
    : base(null) {

        CodTroop = codTroop;
        NamTroop = namTroop;
        VlrDamageDealt = vlrDamageDealt;
        VlrHp = vlrHp;
        VlrScore = vlrScore;
        VlrMotionSpeed = vlrMotionSpeed;
        VlrAttackSpeed = vlrAttackSpeed;
        VlrDropMoney = vlrDropMoney;
        VlrAttackDistance = vlrAttackDistance;
        Objective = troopObjective;
        AttackAtDistance = attackAtDistance;
        IsEnemy = isEnemy;

    }

}