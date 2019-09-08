using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttack : TroopAttackI {

    public PhysicalAttack(IsometricCharacterAnimator isometricCharacterAnimations, RunTimeTroopData troopData) 
        : base(isometricCharacterAnimations, troopData) {

        this.troopData = troopData;
        this.troopAnimations = isometricCharacterAnimations;

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

            yield return new WaitForSecondsRealtime(troopData.vlrAttackSpeed);

            if (entityHealthController == null) {

                yield break;

            }

            entityHealthController.ReceiveDamage(troopData.vlrDamageDealt);

        }

    }

}
