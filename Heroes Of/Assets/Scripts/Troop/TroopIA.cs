using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopIA : MonoBehaviour {

    public Transform targetPoint;

    private TroopStates actualState;

    private Queue<RunTimeTroopData> enemysDetected;

    private RunTimeTroopData troopData;
    private TroopMovementActions movementAction;
    private TroopAttackActions attackAction;

    private void Start() {

        enemysDetected = new Queue<RunTimeTroopData>();

        troopData = GetComponent<RunTimeTroopData>();
        movementAction = GetComponent<TroopMovementActions>();
        attackAction = GetComponent<TroopAttackActions>();

    }

    private void Update() {
        
        if (enemysDetected.Count > 0) {

            RunTimeTroopData enemy = enemysDetected.ToArray()[0];
            Debug.Log("Enemy " + enemy);

            if (actualState != TroopStates.FIGHTING) {

                actualState = TroopStates.FIGHTING;
                StartCoroutine(attackAction.AttackTroop(enemy));

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

    private bool IsCloseToTower() {

        Vector2 towerPosition = targetPoint.position;
        Vector2 actualPosition = transform.position;

        return (Mathf.Ceil(towerPosition.x) != Mathf.Ceil(actualPosition.x) 
            || Mathf.Ceil(towerPosition.y) != Mathf.Ceil(actualPosition.y));

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        Debug.Log("Collision");
        GameObject detectedObject = collision.gameObject;

        switch (detectedObject.tag) {

            case ("Troop"):

                RunTimeTroopData detectedTroop = detectedObject.GetComponent<RunTimeTroopData>();

                if (detectedTroop.isEnemy != troopData.isEnemy) {

                    Debug.Log("Enemy detected");
                    enemysDetected.Enqueue(detectedTroop);

                }
                break;

        }
        
    }

    public void RecieveDamage(int vlrDamageRacived) {

        troopData.vlrHp = troopData.vlrHp - vlrDamageRacived;

        Debug.Log("Hp: " + troopData.vlrHp + " GameObject: " + gameObject);

        if (troopData.vlrHp <= 0) {

            Debug.Log("Troop death");
            //Destroy(gameObject);

        }

    }

}
