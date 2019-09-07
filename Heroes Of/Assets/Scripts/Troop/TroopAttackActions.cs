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

    public void AttackTroop(RunTimeTroopData enemyData) {

        StartCoroutine(attackType.Attack(enemyData));

    }

    /*public IEnumerator AttackTroop(RunTimeTroopData enemyData) {

        Vector2 enemyPosition = enemyData.GameObject.transform.position;
        TroopIA enemyIA = enemyData.GameObject.GetComponent<TroopIA>();

        while (enemyData.vlrHp >= 0) {

            troopAnimations.AnimateAttack(enemyPosition);

            yield return new WaitForSecondsRealtime(troopData.vlrAttackSpeed);

            if (enemyIA == null) {

                yield break;

            }

            enemyIA.ReceiveDamage(troopData.vlrDamageDealt);

        }

    }*/

}
