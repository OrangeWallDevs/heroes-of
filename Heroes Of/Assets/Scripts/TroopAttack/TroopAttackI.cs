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

    public Coroutine Attack(RunTimeTroopData troopData) {

        GameObject enemyTroop = troopData.GameObject;
        HealthController troopHealthController = enemyTroop.GetComponent<TroopHealthController>();

        return attackActions.StartCoroutine(AttackCoroutine(enemyTroop.transform, troopHealthController));

    }

    public Coroutine Attack(RunTimeTowerData towerData) {

        GameObject enemyTower = towerData.GameObject;
        HealthController towerHealthController = enemyTower.GetComponent<TowerHealthController>();

        return attackActions.StartCoroutine(AttackCoroutine(enemyTower.transform, towerHealthController));

    }

    //public Coroutine Attack(RunTimeHeroData heroData) { }

    protected abstract IEnumerator AttackCoroutine(Transform enemy, HealthController enemyHealthController);

}
