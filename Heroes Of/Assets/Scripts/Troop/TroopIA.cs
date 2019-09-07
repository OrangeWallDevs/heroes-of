using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopIA : MonoBehaviour {

    public Transform targetPoint;

    public TroopEvent troopDeathEvent;

    private TroopStates actualState;

    private List<RunTimeTroopData> enemysTroopsDetected;
    private GameObject actualAttackingTarget;

    private RunTimeTroopData troopData;
    private TroopMovementActions movementAction;
    private TroopAttackActions attackAction;

    private void Start() {

        enemysTroopsDetected = new List<RunTimeTroopData>();

        troopData = GetComponent<RunTimeTroopData>();
        movementAction = GetComponent<TroopMovementActions>();
        attackAction = GetComponent<TroopAttackActions>();

        troopDeathEvent.RegisterListener(OnTroopDeath);

    }

    private void Update() {

        if (enemysTroopsDetected.Count > 0) {

            RunTimeTroopData actualTargetEnemy = enemysTroopsDetected.ToArray()[0];
            Vector2 actualTargetEnemyPosition = actualTargetEnemy.transform.position;

            if (IsInRange(actualTargetEnemyPosition, troopData.attackDistance)) { // Attack

                if (actualTargetEnemy.gameObject != actualAttackingTarget) {

                    actualState = TroopStates.FIGHTING;
                    actualAttackingTarget = actualTargetEnemy.gameObject;
                    attackAction.AttackTroop(actualTargetEnemy);

                }

            }
            else { // Get colser to the Enemy

                actualState = TroopStates.MOVING;
                movementAction.MoveToPosition(actualTargetEnemyPosition);

            }

            return;

        }

        if (!IsInTowerPosition()) {

            actualState = TroopStates.MOVING;
            movementAction.MoveToPosition(targetPoint.position);

        }
        else {

            movementAction.WaitOnActualPosition();

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        Transform detectedObject = collision.gameObject.transform.parent;

        if (detectedObject == null) {

            return;

        }

        switch (detectedObject.tag) { // Detect which type of GameObject is

            case ("Troop"):

                RunTimeTroopData detectedTroop = detectedObject.GetComponent<RunTimeTroopData>();

                if (detectedTroop.isEnemy != troopData.isEnemy) {

                    enemysTroopsDetected.Add(detectedTroop);

                }
                break;

            case ("Tower"):

                Debug.Log("Attack Tower");
                break;

            case ("Hero"):

                Debug.Log("Attack Hero");
                break;

        }

    }

    private bool IsInTowerPosition() {

        Vector2 towerPosition = targetPoint.position;
        Vector2 actualPosition = transform.position;

        return !(Mathf.Ceil(towerPosition.x) != Mathf.Ceil(actualPosition.x) 
            || Mathf.Ceil(towerPosition.y) != Mathf.Ceil(actualPosition.y));

    }

    private bool IsInRange(Vector2 targetPosition, float minActionDistance) {

        Vector2 troopPosition = transform.position;

        float distanceBetweenPositions = Vector2.Distance(troopPosition, targetPosition);

        return (distanceBetweenPositions <= minActionDistance);

    }

    public void ReceiveDamage(int vlrDamageReceived) {

        troopData.vlrHp -= vlrDamageReceived;

        if (troopData.vlrHp <= 0) {

            Die();

        }

    }

    public void Die() {

        // Instanciate(deathEffect, transform.position, transform.rotation);
        troopDeathEvent.Raise(troopData);
        Destroy(gameObject);

    }

    private void OnTroopDeath(RunTimeTroopData troop) {

        RunTimeTroopData[] enemysTroopsDetectedCopy = enemysTroopsDetected.ToArray();

        foreach (RunTimeTroopData detectedTroop in enemysTroopsDetectedCopy) {

            if (troop.Equals(detectedTroop)) {

                enemysTroopsDetected.Remove(detectedTroop);

            }

        }

    }

}
