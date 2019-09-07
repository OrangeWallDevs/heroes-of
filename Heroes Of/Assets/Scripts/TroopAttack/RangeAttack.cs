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

    public override IEnumerator Attack(RunTimeTroopData enemyData) {

        while (enemyData.vlrHp > 0) {

            Vector2 enemyPosition = enemyData.GameObject.transform.position;

            troopAnimations.AnimateAttack(enemyPosition);

            // only create at certain point of the animation

            Shoot(enemyPosition); // called by the animation

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
