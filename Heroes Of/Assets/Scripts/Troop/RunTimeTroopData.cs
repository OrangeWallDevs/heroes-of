using UnityEngine;

public class RunTimeTroopData : RunTimeData {

    public string namTroop;
    public int vlrDamageDealt;
    public int vlrHp;
    public int vlrScore;
    public int vlrDropMoney;

    public float vlrMotionSpeed;
    public float vlrAttackSpeed;
    public float attackDistance;
    public PhaseObjectives troopObjective;
    public bool isEnemy;
    public bool attackAtDistance;

    public void SetData(Troop troopData) {

        namTroop = troopData.NamTroop;
        vlrDamageDealt = troopData.VlrDamageDealt;
        vlrHp = troopData.VlrHp;
        vlrScore = troopData.VlrScore;
        vlrDropMoney = troopData.VlrDropMoney;
        vlrMotionSpeed = troopData.VlrMotionSpeed;
        vlrAttackSpeed = troopData.VlrAttackSpeed;
        attackDistance = troopData.VlrAttackDistance;
        troopObjective = troopData.Objective;
        isEnemy = troopData.IsEnemy;
        attackAtDistance = troopData.AttackAtDistance;

    }

}
