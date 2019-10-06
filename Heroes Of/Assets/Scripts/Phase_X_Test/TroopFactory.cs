using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopFactory : MonoBehaviour {

    public TroopScriptableObject[] troopsSO;
    public GameObject troopPrefab;

    public Troop CreateTroop(int codTroop, bool isEnemy, PhaseObjectives troopObjective) {

        Troop troop = new Troop(GameObject.Instantiate(troopPrefab));
        TroopScriptableObject troopData = GetTroopScriptableObject(codTroop);

        troop.CodTroop = troopData.codTroop;
        troop.NamTroop = troopData.namTroop;
        troop.VlrAttackDistance = troopData.vlrAttackDistance;
        troop.VlrAttackSpeed = troopData.vlrAttackSpeed;
        troop.VlrDamageDealt = troop.VlrDamageDealt;
        troop.VlrDropMoney = troopData.vlrDropMoney;
        troop.VlrHp = troopData.vlrHp;
        troop.VlrMotionSpeed = troop.VlrMotionSpeed;
        troop.VlrMotionSpeed = troopData.vlrScore;
        troop.AttackAtDistance = troop.AttackAtDistance;
        troop.Objective = troopObjective;
        troop.IsEnemy = isEnemy;

        troop.GameObject.GetComponentInChildren<SpriteRenderer>().sprite = troopData.sprite;
        troop.GameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = troopData.animatorController;

        troop.GameObject.GetComponent<RunTimeTroopData>().SetData(troop);

        return troop;

    }

    private TroopScriptableObject GetTroopScriptableObject(int codTroop) {

        foreach (TroopScriptableObject troopSO in troopsSO) {

            if (troopSO.codTroop == codTroop) {

                return troopSO;

            }

        }

        return null;

    }

}
