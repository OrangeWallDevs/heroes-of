using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : TroopAttackI {

    private GameObject projectilePrefab;
    private Transform shotingPoint;

    private bool targetIsEnemy;

    public RangeAttack(IsometricCharacterAnimator isometricCharacterAnimations, RunTimeTroopData troopData,
        GameObject projectilePrefab, Transform shotingPoint) : base (isometricCharacterAnimations, troopData) {

        this.troopData = troopData;
        this.troopAnimations = isometricCharacterAnimations;

        this.projectilePrefab = projectilePrefab;
        this.shotingPoint = shotingPoint;

    }

    protected override IEnumerator AttackCoroutine(Transform enemy, HealthController enemyHealthController) {

        switch (enemy.tag) {

            case ("Tower"):

                targetIsEnemy = enemy.GetComponent<RunTimeTowerData>().isEnemy;
                break;

            case ("Troop"):

                targetIsEnemy = enemy.GetComponent<RunTimeTroopData>().isEnemy;
                break;

            case ("Hero"):

                Debug.Log("TO:DO Create a RunTimeData for the Hero");
                break;

        }

        while (enemyHealthController.Health > 0) {

            Vector2 enemyPosition = enemy.position;

            troopAnimations.AnimateAttack(enemyPosition);

            Shoot(enemyPosition);

            yield return new WaitForSecondsRealtime(troopData.vlrAttackSpeed);

        }

    }

    private void Shoot(Vector2 target) {

        GameObject projectile = Object.Instantiate(projectilePrefab, shotingPoint.position, shotingPoint.rotation);
        ProjectileBehaviour projectileActions = projectile.GetComponent<ProjectileBehaviour>();

        if (targetIsEnemy) {

            projectile.layer = 13;

        }
        else {

            projectile.layer = 14;

        }

        projectileActions.Damage = troopData.vlrDamageDealt;
        projectileActions.MoveToTarget(target);

    }

}
