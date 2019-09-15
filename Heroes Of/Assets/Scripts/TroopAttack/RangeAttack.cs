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

    public override IEnumerator Attack(RunTimeData entityData) {

        HealthController entityHealthController = null;
        GameObject entity = entityData.GameObject;

        switch (entity.tag) {

            case ("Troop"):

                entityHealthController = entity.GetComponent<TroopHealthController>();

                RunTimeTroopData troopData = entity.GetComponentInParent<RunTimeTroopData>();
                targetIsEnemy = troopData.isEnemy;
                break;

            case ("Hero"):

                Debug.Log("TO:DO Create a Hero HealthController");
                Debug.Log("TO:DO Create a Hero RunTimeData");
                break;

            case ("Tower"):

                entityHealthController = entity.GetComponent<TowerHealthController>();

                RunTimeTowerData towerData = entity.GetComponentInParent<RunTimeTowerData>();
                targetIsEnemy = towerData.isEnemy;
                break;

        }

        while (entityHealthController.Health > 0) {

            Vector2 enemyPosition = entity.transform.position;

            troopAnimations.AnimateAttack(enemyPosition);

            Shoot(enemyPosition);

            yield return new WaitForSecondsRealtime(troopData.vlrAttackSpeed);

        }

    }

    private void Shoot(Vector2 target) {

        GameObject projectile = Object.Instantiate(projectilePrefab, shotingPoint.position, shotingPoint.rotation);
        ProjectileBehaviour projectileActions = projectile.GetComponent<ProjectileBehaviour>();

        if (targetIsEnemy) {

            projectile.layer = 11;

        }
        else {

            projectile.layer = 12;

        }

        projectileActions.Damage = troopData.vlrDamageDealt;
        projectileActions.MoveToTarget(target);

    }

}
