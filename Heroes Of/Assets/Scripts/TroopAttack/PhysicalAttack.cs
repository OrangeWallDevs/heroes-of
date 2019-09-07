using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttack : TroopAttackI {

    public PhysicalAttack(IsometricCharacterAnimator isometricCharacterAnimations, RunTimeTroopData troopData) 
        : base(isometricCharacterAnimations, troopData) {

        this.troopData = troopData;
        this.troopAnimations = isometricCharacterAnimations;

    }

    public override IEnumerator Attack(RunTimeTroopData enemyData) {

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

    }
}
