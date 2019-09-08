using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopIA : MonoBehaviour {

    public TroopEvent troopDeathEvent;
    public TowerEvent towerBeingAttackedEvent, towerDestroyedEvent;

    private TroopStates actualState;

    private List<RunTimeTroopData> enemysTroopsDetected;
    private GameObject actualAttackingTarget;

    private List<RunTimeTowerData> towersList;
    private RunTimeTowerData detectedEnemyTower;
    private GameObject actualTargetTowerToWalk, lastAttackedTower;

    private RunTimeTroopData troopData;
    private TroopMovementActions movementAction;
    private TroopAttackActions attackAction;

    private void Awake() {

        GameObject[] towersInGame = GameObject.FindGameObjectsWithTag("Tower");
        float[] distancesBetweenTower = new float[towersInGame.Length];

        Vector2 troopPosition = transform.position;

        for (int i = 0; i < towersInGame.Length; i++) {

            Vector2 towerPosition = towersInGame[i].transform.position;
            distancesBetweenTower[i] = Vector2.Distance(troopPosition, towerPosition);

        }

        for (int i = 1; i < towersInGame.Length; i++) { // Order the towers by distance

            float lastComparationDistance = distancesBetweenTower[i];
            GameObject lastComparationTower = towersInGame[i];

            int j = i - 1;

            while (j >= 0 && distancesBetweenTower[j] > lastComparationDistance) {

                distancesBetweenTower[j + 1] = distancesBetweenTower[j];
                towersInGame[j + 1] = towersInGame[j];

                j--;

            }

            distancesBetweenTower[j + 1] = lastComparationDistance;
            towersInGame[j + 1] = lastComparationTower;

        }

        towersList = new List<RunTimeTowerData>();

        foreach (GameObject tower in towersInGame) {

            towersList.Add(tower.GetComponent<RunTimeTowerData>());

        }

    }

    private void Start() {

        enemysTroopsDetected = new List<RunTimeTroopData>();

        troopData = GetComponent<RunTimeTroopData>();
        movementAction = GetComponent<TroopMovementActions>();
        attackAction = GetComponent<TroopAttackActions>();
        
        troopDeathEvent.RegisterListener(OnTroopDeath);
        towerDestroyedEvent.RegisterListener(OnTowerDestruction);

    }

    private void Update() {

        if (enemysTroopsDetected.Count > 0) {

            HandleEnemyTroopAttack();

        } 
        else if (towersList.Count > 0) {

            if (troopData.troopObjective == PhaseObjectives.DEFEND) {

                HandleTowerDefense();

            }
            else {

                HandleTowerAttack();

            }

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

                RunTimeTowerData detectedTower = detectedObject.GetComponent<RunTimeTowerData>();

                if (detectedTower.isEnemy != troopData.isEnemy) {

                    detectedEnemyTower = detectedTower;

                }
                break;

            case ("Hero"):

                Debug.Log("Attack Hero");
                break;

        }

    }

    private void HandleEnemyTroopAttack() {

        RunTimeTroopData actualTargetEnemy = enemysTroopsDetected.ToArray()[0];
        Vector2 actualTargetEnemyPosition = actualTargetEnemy.transform.position;

        if (IsInRange(actualTargetEnemyPosition, troopData.attackDistance)) { // Attack

            if (actualTargetEnemy.gameObject != actualAttackingTarget) {

                actualState = TroopStates.FIGHTING;
                actualAttackingTarget = actualTargetEnemy.gameObject;
                attackAction.Attack(actualTargetEnemy);

            }

        }
        else { // Get colser to the Enemy

            actualState = TroopStates.MOVING;
            movementAction.MoveToPosition(actualTargetEnemyPosition);

        }

        return;

    }

    private void HandleTowerDefense() {

        actualTargetTowerToWalk = FindTargetTower();

        if (!IsInTowerPosition()) {

            actualState = TroopStates.MOVING;
            movementAction.MoveToPosition(actualTargetTowerToWalk.transform.position);

        } 
        else {

            movementAction.WaitOnActualPosition();

        }

    }

    private void HandleTowerAttack() {

        actualTargetTowerToWalk = FindTargetTower();

        if (IsInRange(actualTargetTowerToWalk.transform.position, troopData.attackDistance)) {

            if (lastAttackedTower != detectedEnemyTower.GameObject) {
                
                actualState = TroopStates.ATTACKING;

                lastAttackedTower = detectedEnemyTower.GameObject;
                towerBeingAttackedEvent.Raise(detectedEnemyTower);
                attackAction.Attack(detectedEnemyTower);

            }

        } 
        else {

            actualState = TroopStates.MOVING;

            movementAction.MoveToPosition(actualTargetTowerToWalk.transform.position);

        }

    }

    private bool IsInRange(Vector2 targetPosition, float minActionDistance) {

        Vector2 troopPosition = transform.position;

        float distanceBetweenPositions = Vector2.Distance(troopPosition, targetPosition);

        return (distanceBetweenPositions <= minActionDistance);

    }

    private GameObject FindTargetTower() {

        GameObject targetTower;

        if (troopData.troopObjective == PhaseObjectives.ATTACK) { // Get the closest tower as target

            targetTower = towersList.ToArray()[0].GameObject;

        } 
        else { // Get the most distant tower as target

            targetTower = towersList.ToArray()[towersList.Count - 1].GameObject;

        }

        return targetTower;

    }

    private bool IsInTowerPosition() {

        Vector2 towerPosition = actualTargetTowerToWalk.transform.position;
        Vector2 actualPosition = transform.position;

        return !(Mathf.Ceil(towerPosition.x) != Mathf.Ceil(actualPosition.x)
            || Mathf.Ceil(towerPosition.y) != Mathf.Ceil(actualPosition.y));

    }

    private void OnTroopDeath(RunTimeTroopData troop) {

        RunTimeTroopData[] enemysTroopsDetectedCopy = enemysTroopsDetected.ToArray();

        foreach (RunTimeTroopData detectedTroop in enemysTroopsDetectedCopy) {

            if (troop.Equals(detectedTroop)) {

                enemysTroopsDetected.Remove(detectedTroop);

            }

        }

    }

    private void OnTowerDestruction(RunTimeTowerData tower) {

        if (detectedEnemyTower == tower) {

            detectedEnemyTower = null;

        }

        towersList.Remove(tower);

    }

}
