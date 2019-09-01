using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopAttackActions : MonoBehaviour {

    private RunTimeTroopData troopData;
    private IsometricCharacterAnimator troopAnimations;

    void Start() {

        troopData = GetComponent<RunTimeTroopData>();
        troopAnimations = GetComponentInChildren<IsometricCharacterAnimator>();
        
    }

    public IEnumerator AttackTroop(RunTimeTroopData enemyData) {

        Vector2 enemyPosition = enemyData.GameObject.transform.position;
        TroopIA enemyIA = enemyData.GameObject.GetComponent<TroopIA>();

        while (enemyData.vlrHp >= 0) {

            troopAnimations.AnimateAttack(enemyPosition);

            yield return new WaitForSecondsRealtime(troopData.vlrAttackSpeed);

            enemyIA.RecieveDamage(troopData.vlrDamageDealt);

        }

    }

}
