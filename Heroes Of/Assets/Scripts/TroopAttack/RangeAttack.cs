using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : TroopAttackI {

    private GameObject projectilePrefab;
    private Transform shotingPoint;

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
                break;

            case ("Hero"):

                Debug.Log("TO:DO Create a Hero HealthController");
                break;

            case ("Tower"):

                entityHealthController = entity.GetComponent<TowerHealthController>();
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

        projectileActions.Damage = troopData.vlrDamageDealt;
        projectileActions.MoveToTarget(target);

    }

}
