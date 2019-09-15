using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttack : TroopAttackI {

    public PhysicalAttack(IsometricCharacterAnimator isometricCharacterAnimations, RunTimeTroopData troopData) 
        : base(isometricCharacterAnimations, troopData) {

        this.troopData = troopData;
        this.troopAnimations = isometricCharacterAnimations;

    }

    protected override IEnumerator AttackCoroutine(Transform enemy, HealthController enemyHealthController) {

        while (enemyHealthController.Health > 0) {

            Vector2 enemyPosition = enemy.position;

            troopAnimations.AnimateAttack(enemyPosition);

            yield return new WaitForSecondsRealtime(troopData.vlrAttackSpeed);

            if (enemyHealthController == null) {

                yield break;

            }

            enemyHealthController.ReceiveDamage(troopData.vlrDamageDealt);

        }

    }

}
