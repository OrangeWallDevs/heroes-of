using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopFactory : MonoBehaviour {

    public TroopScriptableObject[] troopsSO;
    public GameObject troopPrefab;

    public Troop CreateTroop(int codTroop, bool isEnemy, PhaseObjectives troopObjective) {

        Troop troop = new Troop();
        TroopScriptableObject troopData = GetTroopScriptableObject(codTroop);

        troop.CodTroop = troopData.codTroop;
        troop.NamTroop = troopData.namTroop;
        troop.ValAttackDistance = troopData.valAttackDistance;
        troop.ValAttackSpeed = troopData.valAttackSpeed;
        troop.ValDamageDealt = troopData.valDamageDealt;
        troop.ValDropMoney = troopData.valDropMoney;
        troop.ValHp = troopData.valHp;
        troop.ValMotionSpeed = troopData.valMorionSpeed;
        troop.ValScore = troopData.valScore;
        troop.IdtAttackAtDistance = troopData.attackAtDistance;
        troop.GameObject = GameObject.Instantiate(troopPrefab);
        troop.Objective = troopObjective;
        troop.IsEnemy = isEnemy;

        troop.GameObject.GetComponentInChildren<SpriteRenderer>().sprite = troopData.sprite;
        troop.GameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = troopData.animatorController;

        if (troop.IdtAttackAtDistance && troopData.projectilePrefab != null) {

            troop.GameObject.GetComponent<TroopAttackActions>().projectilePrefab = troopData.projectilePrefab;

        }

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
