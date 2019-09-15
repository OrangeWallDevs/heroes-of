using System.Collections;
using UnityEngine;

public abstract class TroopAttackI {

    protected IsometricCharacterAnimator troopAnimations;
    protected RunTimeTroopData troopData;
    protected TroopAttackActions attackActions;

    public TroopAttackI(IsometricCharacterAnimator isometricCharacterAnimations, RunTimeTroopData troopData) {

        troopAnimations = isometricCharacterAnimations;
        attackActions = troopData.GetComponent<TroopAttackActions>();

    }

    public Coroutine Attack(GameObject entityAttacked) {

        HealthController entityHealthController = entityAttacked.GetComponent<HealthController>();

        return attackActions.StartCoroutine(AttackCoroutine(entityAttacked.transform, entityHealthController));

    }

    protected abstract IEnumerator AttackCoroutine(Transform enemy, HealthController enemyHealthController);

}
