using UnityEngine;

public class RunTimeTroopData : RunTimeData {

    public string namTroop;
    public int valDamageDealt;
    public int valHp;
    public int valScore;
    public int valDropMoney;

    public float valMotionSpeed;
    public float valAttackSpeed;
    public float attackDistance;
    public PhaseObjectives troopObjective;
    public bool isEnemy;
    public bool attackAtDistance;

    public void SetData(Troop troopData) {

        namTroop = troopData.NamTroop;
        valDamageDealt = troopData.ValDamageDealt;
        valHp = troopData.ValHp;
        valScore = troopData.ValScore;
        valDropMoney = troopData.ValDropMoney;
        valMotionSpeed = troopData.ValMotionSpeed;
        valAttackSpeed = troopData.ValAttackSpeed;
        attackDistance = troopData.ValAttackDistance;
        troopObjective = troopData.Objective;
        isEnemy = troopData.IsEnemy;
        attackAtDistance = troopData.IdtAttackAtDistance;

    }

}
