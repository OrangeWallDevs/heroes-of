using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopIA : MonoBehaviour {

    public Transform targetPoint;

    private TroopStates actualState;

    private List<RunTimeTroopData> enemysTroopsDetected;

    private RunTimeTroopData troopData;
    private TroopMovementActions movementAction;
    private TroopAttackActions attackAction;

    private void Start() {

        enemysTroopsDetected = new List<RunTimeTroopData>();

        troopData = GetComponent<RunTimeTroopData>();
        movementAction = GetComponent<TroopMovementActions>();
        attackAction = GetComponent<TroopAttackActions>();

    }

    private void Update() {
        
        if (enemysTroopsDetected.Count > 0) {

            RunTimeTroopData actualTargetEnemy = enemysTroopsDetected.ToArray()[0];
            Debug.Log("Enemy " + actualTargetEnemy);

            Vector2 actualTargetEnemyPosition = actualTargetEnemy.transform.position;
            Debug.Log(IsInRange(actualTargetEnemyPosition, 3));

            // Check the distance between thee troops isInRange()
            if (IsInRange(actualTargetEnemyPosition, 3)) {

                // Can attack

            }
            else {

                // Get Closer

            }

            if (actualState != TroopStates.FIGHTING) {

                actualState = TroopStates.FIGHTING;
                StartCoroutine(attackAction.AttackTroop(actualTargetEnemy));

            }

            return;

        }

        if (IsCloseToTower()) {

            actualState = TroopStates.MOVING;
            movementAction.MoveToPosition(targetPoint.position);

        }
        else {

            movementAction.WaitOnActualPosition();

        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        Debug.Log("Collision");
        GameObject detectedObject = collision.gameObject;

        switch (detectedObject.tag) { // Detect which type of gameObject is

            case ("Troop"):

                // Get the proper data of the troop
                RunTimeTroopData detectedTroop = detectedObject.GetComponent<RunTimeTroopData>();

                if (detectedTroop.isEnemy != troopData.isEnemy) {

                    Debug.Log("Enemy detected");
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

    private bool IsCloseToTower() {

        Vector2 towerPosition = targetPoint.position;
        Vector2 actualPosition = transform.position;

        return (Mathf.Ceil(towerPosition.x) != Mathf.Ceil(actualPosition.x) 
            || Mathf.Ceil(towerPosition.y) != Mathf.Ceil(actualPosition.y));

    }

    private bool IsInRange(Vector2 targetPosition, float minActionDistance) {

        Vector2 troopPosition = transform.position;

        float distanceBetweenPositions = Mathf.Sqrt(Mathf.Pow((targetPosition.x - troopPosition.x), 2) 
            + Mathf.Pow((targetPosition.y - troopPosition.y), 2));

        return (distanceBetweenPositions <= minActionDistance);

    }

    public void ReceiveDamage(int vlrDamageReceived) {

        troopData.vlrHp -= vlrDamageReceived;

        Debug.Log("Hp: " + troopData.vlrHp + " GameObject: " + gameObject);

        if (troopData.vlrHp <= 0) {

            Debug.Log("Troop death");
            // Raise event to remove troop from others lists and add money
            // Destroy(gameObject);

        }

    }

    private void OnTroopDeath(RunTimeTroopData troop) {

        foreach (RunTimeTroopData detectedTroop in enemysTroopsDetected) {

            if (troop.Equals(detectedTroop)) {

                enemysTroopsDetected.Remove(detectedTroop);

            }

        }

    }

}
