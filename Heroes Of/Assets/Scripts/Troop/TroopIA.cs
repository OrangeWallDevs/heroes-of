using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class TroopIA : MonoBehaviour {

    public int towerPrioritieWeight, heroPrioritieWeight, troopPrioritieWeight;

    public TroopEvent troopDeathEvent, attackingTowerEvent;
    public TowerEvent towerDestroyedEvent;
    public HeroEvent heroDeathEvent;

    private TroopStates actualState;

    private GameObject actualAttackingTarget;

    private List<RunTimeTroopData> enemysTroopsDetected;
    private RunTimeTroopData detectedEnemyTroop;

    private RunTimeHeroData detectedEnemyHero;

    private List<RunTimeTowerData> towersList;
    private RunTimeTowerData detectedEnemyTower;
    private GameObject actualTargetTowerToWalk;

    private RunTimeTroopData troopData;
    private TroopMovementActions movementAction;
    private TroopAttackActions attackAction;

    private UnityAction actualAction;

    private int count = 0;

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

        actualAction = null;

        enemysTroopsDetected = new List<RunTimeTroopData>();

        troopData = GetComponent<RunTimeTroopData>();
        movementAction = GetComponent<TroopMovementActions>();
        attackAction = GetComponent<TroopAttackActions>();
        
        troopDeathEvent.RegisterListener(OnTroopDeath);
        towerDestroyedEvent.RegisterListener(OnTowerDestruction);
        attackingTowerEvent.RegisterListener(OnTroopAttackingTower);
        heroDeathEvent.RegisterListener(OnHeroDeath);

    }

    private void Update() {

        if (actualAction == null) {

            actualAction = pickAnAction();

        }
        else {

            actualAction();

        }

        /*if (enemysTroopsDetected.Count > 0) {

            HandleEnemyTroopAttack();

        } 
        else if (detectedEnemyHero != null) {

            HandleEnemyHeroAttack();

        }
        else if (towersList.Count > 0) {

            if (troopData.troopObjective == PhaseObjectives.DEFEND) {

                HandleTowerDefense();

            }
            else {

                HandleTowerAttack();

            }

        }*/

        if (attackAction.IsAttacking && actualState == TroopStates.ATTACKING) {

            attackingTowerEvent.Raise(troopData);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        Transform detectedObject = collision.gameObject.transform.parent;

        if (detectedObject != null) {

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

                    RunTimeHeroData detectedHero = detectedObject.GetComponent<RunTimeHeroData>();

                    if (detectedHero.isEnemy != troopData.isEnemy) {

                        detectedEnemyHero = detectedHero;

                    }
                    break;

            }

        }

    }

    /*private void OnTriggerExit2D(Collider2D collision) {

        Transform objectLeaving = collision.gameObject.transform.parent;
         
        // Refactory this later
        if (objectLeaving == actualAttackingTarget.transform) {

            attackAction.StopAttack();
            actualAttackingTarget = null;
            detectedEnemyHero = null;
            actualAction = null;

        }

    }*/

    private UnityAction pickAnAction() {

        Dictionary<UnityAction, int> validActions = new Dictionary<UnityAction, int>();

        if (towersList.Count > 0) {

            if (troopData.troopObjective == PhaseObjectives.ATTACK) {
    
                validActions.Add(HandleTowerAttack, towerPrioritieWeight);
    
            }
            else {
    
                validActions.Add(HandleTowerDefense, towerPrioritieWeight);
    
            }

        }

        if (detectedEnemyHero != null) {

            validActions.Add(HandleEnemyHeroAttack, heroPrioritieWeight);

        }

        if (enemysTroopsDetected.Count > 0) {

            validActions.Add(HandleEnemyTroopAttack, troopPrioritieWeight);

        }

        int sumPriorities = troopPrioritieWeight + heroPrioritieWeight + towerPrioritieWeight;
        int randomResult = (int) Mathf.Ceil(Random.Range(0, sumPriorities));

        UnityAction resultAction = null;
        int lastPrioritieWeight = 0;

        List<KeyValuePair<UnityAction, int>> actionsSortedList = validActions.ToList<KeyValuePair<UnityAction, int>>();
        actionsSortedList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

        foreach (KeyValuePair<UnityAction, int> pair in validActions) {

            if (pair.Equals(actionsSortedList.Last())) {

                if (randomResult >= lastPrioritieWeight) {

                    resultAction = pair.Key;

                }

            }
            else if ((randomResult >= lastPrioritieWeight && randomResult < pair.Value)) {

                resultAction = pair.Key;
                break;

            }

            lastPrioritieWeight = pair.Value;

        }

        return resultAction;

    }

    private void HandleEnemyTroopAttack() {

        Debug.Log("Attack a troop, by: " + gameObject.name);

        RunTimeTroopData actualTargetEnemy = enemysTroopsDetected.ToArray()[0];
        Vector2 actualTargetEnemyPosition = actualTargetEnemy.transform.position;

        if (IsInRange(actualTargetEnemyPosition, troopData.attackDistance)) { // Attack

            if (actualTargetEnemy.gameObject != actualAttackingTarget && !attackAction.IsAttacking) {

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

    private void HandleEnemyHeroAttack() {

        Debug.Log("Attack a hero, by: " + gameObject.name);

        Vector2 enemyHeroPosition = detectedEnemyHero.GameObject.transform.position;

        if (IsInRange(enemyHeroPosition, troopData.attackDistance)) {

            if (actualAttackingTarget != detectedEnemyHero.GameObject && !attackAction.IsAttacking) {

                actualState = TroopStates.FIGHTING;

                actualAttackingTarget = detectedEnemyHero.GameObject;
                attackAction.Attack(detectedEnemyHero);
                count++;
                Debug.Log(count);

            }

        }
        else {

            actualState = TroopStates.MOVING;

            movementAction.MoveToPosition(enemyHeroPosition);

        }

    }

    private void HandleTowerDefense() {

        Debug.Log("Defend a tower, by: " + gameObject.name);

        actualTargetTowerToWalk = FindTargetTower();

        if (!IsInTowerPosition()) {

            actualState = TroopStates.MOVING;

            movementAction.MoveToPosition(actualTargetTowerToWalk.transform.position);

        } 
        else {

            actualState = TroopStates.DEFENDING;

            movementAction.WaitOnActualPosition();

        }

        actualAction = null;

    }

    private void HandleTowerAttack() {

        Debug.Log("Attack a tower, by: " + gameObject.name);

        actualTargetTowerToWalk = FindTargetTower();

        if (IsInRange(actualTargetTowerToWalk.transform.position, troopData.attackDistance) && detectedEnemyTower != null) {

            if (actualAttackingTarget != detectedEnemyTower.gameObject && !attackAction.IsAttacking) {

                actualState = TroopStates.ATTACKING;

                actualAttackingTarget = detectedEnemyTower.GameObject;
                attackAction.Attack(detectedEnemyTower);

            }

        } 
        else {

            actualState = TroopStates.MOVING;

            movementAction.MoveToPosition(actualTargetTowerToWalk.transform.position);
            actualAction = null;

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

            if (troop == detectedTroop) {

                enemysTroopsDetected.Remove(detectedTroop);

                if (actualAttackingTarget == troop.GameObject) {

                    attackAction.StopAttack();
                    actualAction = null;

                }

            }

        }

    }

    private void OnTowerDestruction(RunTimeTowerData tower) {

        if (detectedEnemyTower == tower) {

            detectedEnemyTower = null;

            if (actualAttackingTarget == tower.GameObject) {

                attackAction.StopAttack();
                actualAction = null;

            }

        }

        towersList.Remove(tower);

    }

    private void OnHeroDeath(RunTimeHeroData hero) {

        if (actualAttackingTarget == hero.GameObject) {

            attackAction.StopAttack();
            actualAction = null;

        }

        if (detectedEnemyHero == hero) {

            detectedEnemyHero = null;

        }

    }

    private void OnTroopAttackingTower(RunTimeTroopData troop) {

        if (troopData.troopObjective == PhaseObjectives.DEFEND && actualState == TroopStates.DEFENDING) {

            if (!enemysTroopsDetected.Contains(troop)) {

                enemysTroopsDetected.Add(troop);

            }

        }

    }

}
