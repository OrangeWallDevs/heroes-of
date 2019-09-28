using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopIA : MonoBehaviour {

    public int towerPriorityWeight, heroPriorityWeight, troopPriorityWeight;

    public TroopEvent troopDeathEvent, attackingTowerEvent;
    public TowerEvent towerDestroyedEvent;
    public HeroEvent heroDeathEvent;

    private TroopStates actualState;

    private Transform actualTarget;
    private float actualTargetPriority;
    private List<Transform> closeTargets;
    private List<Transform> towersList;

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

        towersList = new List<Transform>();

        foreach (GameObject tower in towersInGame) {

            towersList.Add(tower.GetComponent<Transform>());

        }

    }

    private void Start() {

        closeTargets = new List<Transform>();

        troopData = GetComponent<RunTimeTroopData>();
        movementAction = GetComponent<TroopMovementActions>();
        attackAction = GetComponent<TroopAttackActions>();

        troopDeathEvent.RegisterListener(OnTroopDeath);
        towerDestroyedEvent.RegisterListener(OnTowerDestruction);
        attackingTowerEvent.RegisterListener(OnTroopAttackingTower);
        heroDeathEvent.RegisterListener(OnHeroDeath);

    }

    private void Update() {

        if (ActualTarget == null) {

            if (actualState != TroopStates.DEFENDING) {

                ActualTarget = PickAnTarget();

            }

        } 
        else {

            if (!IsInRange(ActualTarget.position, troopData.attackDistance)) {

                MoveTo(ActualTarget.position);

            }
            else {

                if (troopData.troopObjective == PhaseObjectives.DEFEND && ActualTarget.tag == "Tower") {

                    EnterInDefenseState();

                }
                else if (!attackAction.IsAttacking) {

                    AttackTarget(ActualTarget);

                }

            }

        }

        if (attackAction.IsAttacking && actualState == TroopStates.ATTACKING) {

            attackingTowerEvent.Raise(troopData);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        Transform detectedObject = collision.gameObject.transform.parent;
        float detectedObjectPriority;

        if (detectedObject != null) {

            if (TargetIsEnemy(detectedObject)) {

                detectedObjectPriority = GetTargetPriority(detectedObject);
    
                if (detectedObjectPriority > actualTargetPriority) {
    
                    if (ActualTarget != null) {
    
                        closeTargets.Add(ActualTarget);
    
                    }
    
                    if (attackAction.IsAttacking) {
    
                        attackAction.StopAttack();
    
                    }
    
                    ActualTarget = detectedObject;
    
                }
                else {
    
                    closeTargets.Add(detectedObject);
    
                }

            }

        }

    }

    private bool TargetIsEnemy(Transform detectedObject) {

        bool isEnemy = false;

        switch (detectedObject.tag) {

            case ("Tower"):

                isEnemy = detectedObject.GetComponent<RunTimeTowerData>().isEnemy;
                break;

            case ("Hero"):

                isEnemy = detectedObject.GetComponent<RunTimeHeroData>().isEnemy;
                break;

            case ("Troop"):

                isEnemy = detectedObject.GetComponent<RunTimeTroopData>().isEnemy;
                break;

        }

        return isEnemy;

    }

    private void OnTriggerExit2D(Collider2D collision) {

        Transform objectOutOfRadio = collision.gameObject.transform.parent;

        if (objectOutOfRadio != null) {

            HandleEnemyLeavedAttackRadio(objectOutOfRadio);

        }

    }

    private Transform PickAnTarget() {

        Transform nextTarget = null;

        if (closeTargets.Count <= 0 && towersList.Count > 0) {

            nextTarget = FindTargetTower();

        }
        else {

            // Pega um aleatório do closeTargets de acordo com a prioridade.
            // TO:DO --> Ver como incluir a torre nessa seleção aleatória.

            int randomNumber = Random.Range(0, closeTargets.Count - 1);
            Transform[] targets = closeTargets.ToArray();

            nextTarget = targets[randomNumber];
            closeTargets.Remove(nextTarget);

        }

        return nextTarget;

    }

    private int GetTargetPriority(Transform target) {

        int priority = 0;

        // TO:DO --> Find a better way to set the priority weight
        if (target == null) {

            priority = 0;

        } 
        else {

            switch (target.tag) {
    
                case ("Tower"):

                    priority = towerPriorityWeight;
                    break;
    
                case ("Hero"):

                    priority = heroPriorityWeight;
                    break;
    
                case ("Troop"):

                    priority = troopPriorityWeight;
                    break;
    
            }

        }

        return priority;

    }

    private void EnterInDefenseState() {

        actualState = TroopStates.DEFENDING;

        ActualTarget = null;

        movementAction.WaitOnActualPosition();
        
    }

    private void AttackTarget(Transform attackTarget) {

        if (attackTarget.tag == "Tower") {

            actualState = TroopStates.ATTACKING;

        } else if (attackTarget.tag == "Troop" || attackTarget.tag == "Hero") {

            actualState = TroopStates.FIGHTING;

        }

        RunTimeData targetData = attackTarget.GetComponent<RunTimeData>();
        attackAction.Attack(targetData);

    }

    private void MoveTo(Vector2 position) {

        movementAction.MoveToPosition(position);

    }

    private bool IsInRange(Vector2 targetPosition, float minActionDistance) {

        Vector2 troopPosition = transform.position;

        float distanceBetweenPositions = Vector2.Distance(troopPosition, targetPosition);

        return (distanceBetweenPositions <= minActionDistance);

    }

    private Transform FindTargetTower() {

        Transform targetTower;

        if (troopData.troopObjective == PhaseObjectives.ATTACK) { // Get the closest tower as target

            targetTower = towersList.ToArray()[0];

        } else { // Get the most distant tower as target

            targetTower = towersList.ToArray()[towersList.Count - 1];

        }

        return targetTower;

    }

    private void OnTroopDeath(RunTimeTroopData troop) {

        Transform troopTransform = troop.GameObject.transform;

        HandleEnemyLeavedAttackRadio(troopTransform);

    }

    private void OnTowerDestruction(RunTimeTowerData tower) {

        Transform towerTransform = tower.GameObject.transform;

        HandleEnemyLeavedAttackRadio(towerTransform);

        towersList.Remove(towerTransform);

    }

    private void OnHeroDeath(RunTimeHeroData hero) {

        Transform heroTransform = hero.GameObject.transform;

        HandleEnemyLeavedAttackRadio(heroTransform);

    }

    private void HandleEnemyLeavedAttackRadio(Transform enemy) {

        if (closeTargets.Contains(enemy)) {

            closeTargets.Remove(enemy);

        }

        if (ActualTarget == enemy) {

            if (attackAction.IsAttacking) {

                attackAction.StopAttack();

            }

            ActualTarget = null;

        }

    }

    private void OnTroopAttackingTower(RunTimeTroopData troop) {

        if (troopData.troopObjective == PhaseObjectives.DEFEND) {

            Transform enemyAttackingTower = troop.GameObject.transform;

            if (!closeTargets.Contains(enemyAttackingTower)) {

                closeTargets.Add(enemyAttackingTower);

            }

            if (!attackAction.IsAttacking && ActualTarget == null) {

                ActualTarget = PickAnTarget();

            }

        }
        
    }

    public Transform ActualTarget {

        get { return actualTarget; }

        private set {

            actualTarget = value;
            actualTargetPriority = GetTargetPriority(actualTarget);

        }

    }

}