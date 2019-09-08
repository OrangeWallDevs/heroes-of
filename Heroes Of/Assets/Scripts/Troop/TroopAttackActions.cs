using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopAttackActions : MonoBehaviour {

    public GameObject projectilePrefab = null;
    public Transform shotingPoint = null;

    private RunTimeTroopData troopData;
    private IsometricCharacterAnimator troopAnimations;

    private TroopAttackI attackType;

    void Start() {

        troopData = GetComponent<RunTimeTroopData>();
        troopAnimations = GetComponentInChildren<IsometricCharacterAnimator>();

        if (troopData.attackAtDistance) {

            attackType = new RangeAttack(troopAnimations, troopData, projectilePrefab, shotingPoint);

        }
        else {

            attackType = new PhysicalAttack(troopAnimations, troopData);

        }
        
    }

    public void Attack(RunTimeData entityData) {

        StartCoroutine(attackType.Attack(entityData));

    }

}
