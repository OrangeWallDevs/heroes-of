using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopIA : MonoBehaviour {

    public int towerPriorityWeight, heroPriorityWeight, troopPriorityWeight;

    public GameEvent waveEndEvent;
    public TroopEvent troopDeathEvent, troopRemovedOnWaveEndEvent, attackingTowerEvent;
    public TowerEvent towerDestroyedEvent;
    public HeroEvent heroDeathEvent;

    private TroopStates actualState;

    private Transform actualTarget;
    private float actualTargetPriority;
    private List<Transform> closeTargets;

    private PriorityList<Transform> construcionsList;

    private RunTimeTroopData troopData;
    private TroopMovementActions movementAction;
    private TroopAttackActions attackAction;

    private void Awake() {

        construcionsList = new PriorityList<Transform>();

        foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower")) {

            int towerOrder = tower.GetComponent<TowerAttackOrder>().orderInAttack;

            PriorityPair<Transform> towerPair = new PriorityPair<Transform>(tower.transform, towerOrder);

            construcionsList.Add(towerPair);

        }

        GameObject core = GameObject.FindGameObjectWithTag("Core");
        int lastTowerOrder = 0;

        if (construcionsList.GetFirst() != null) {

            lastTowerOrder = construcionsList.GetFirst().Priority;

        }

        PriorityPair<Transform> corePair = new PriorityPair<Transform>(core.transform, lastTowerOrder + 1);
        construcionsList.Add(corePair);

        troopData = GetComponent<RunTimeTroopData>(); //TO:DO adapt to use Troop class with RunTimeData data
        movementAction = GetComponent<TroopMovementActions>();
        attackAction = GetComponent<TroopAttackActions>();

    }

    private void Start() {

        closeTargets = new List<Transform>();

        waveEndEvent.RegisterListener(OnWaveEnd);
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

                actualState = TroopStates.MOVING;
                movementAction.actualTarget = ActualTarget.position;

            }
            else {

                if (troopData.troopObjective == PhaseObjectives.DEFEND && ActualTarget.CompareTag("Tower")) {

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

            // TO:DO use Troop data to find if the detected object is an enemy
            if (troopData.isEnemy != TargetIsEnemy(detectedObject)) {

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
                else if (!closeTargets.Contains(detectedObject)){
    
                    closeTargets.Add(detectedObject);
    
                }

            }

        }

    }

    private void OnDestroy() {

        ActualTarget = null;

        if (attackAction.IsAttacking) {

            attackAction.StopAttack();

        }

        closeTargets.Clear();
        construcionsList.Clear();

        waveEndEvent.UnregisterListener(OnWaveEnd);
        troopDeathEvent.UnregisterListener(OnTroopDeath);
        towerDestroyedEvent.UnregisterListener(OnTowerDestruction);
        attackingTowerEvent.UnregisterListener(OnTroopAttackingTower);
        heroDeathEvent.UnregisterListener(OnHeroDeath);

        enabled = false;

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

        if (closeTargets.Count <= 0 && construcionsList.Count > 0) {

            nextTarget = GetNextTargetBuilding();

        }
        else if (closeTargets.Count > 0){

            nextTarget = GetNextCloseTarget();

        }

        return nextTarget;

    }

    private int GetTargetPriority(Transform target) {

        int priority = 0;

        if (target != null) {

            switch (target.tag) {
    
                case ("Tower"):

                    priority = TowerPriorityWeight;
                    break;
    
                case ("Hero"):

                    priority = HeroPriorityWeight;
                    break;
    
                case ("Troop"):

                    priority = TroopPriorityWeight;
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

        if (attackTarget.CompareTag("Tower")) {

            actualState = TroopStates.ATTACKING;

        } 
        else if (attackTarget.CompareTag("Troop") || attackTarget.CompareTag("Hero")) {

            actualState = TroopStates.FIGHTING;

        }

        // TO:DO use Entity class from RunTimeDate to pass on attackAction.Attack()
        RunTimeData targetData = attackTarget.GetComponent<RunTimeData>();
        if (targetData != null) {

            attackAction.Attack(targetData);

        }

    }

    private bool IsInRange(Vector2 targetPosition, float minActionDistance) {

        Vector2 troopPosition = transform.position;

        float distanceBetweenPositions = Vector2.Distance(troopPosition, targetPosition);

        return (distanceBetweenPositions <= minActionDistance);

    }

    private Transform GetNextTargetBuilding() {

        return construcionsList.GetLast().Key;

    }

    private Transform GetNextCloseTarget() {

        PriorityList<string> tagsPriority = new PriorityList<string>();

        foreach (Transform target in closeTargets) {

            if (target != null) {

                if (!tagsPriority.ContainsKey(target.tag)) {

                    PriorityPair<string> pair = new PriorityPair<string>(target.tag, GetTargetPriority(target));
                    tagsPriority.Add(pair);

                }

            }

        }

        int randomNumber = Random.Range(0, TroopPriorityWeight + TowerPriorityWeight + HeroPriorityWeight);
        int lastPriority = 0;
        string selectedTag = "";

        for (int i = tagsPriority.Count - 1; i >= 0; i--) {

            PriorityPair<string> pair = tagsPriority.Find(i);

            if (i == 0) {

                if (randomNumber >= lastPriority) {

                    selectedTag = pair.Key;

                }

            } 
            else {

                if (randomNumber >= lastPriority && randomNumber < pair.Priority) {

                    selectedTag = pair.Key;
                    break;

                }

            }

            lastPriority = pair.Priority;

        }

        List<Transform> possibleTargets = new List<Transform>();

        foreach (Transform target in closeTargets) {

            if (target != null) {

                if (target.tag == selectedTag) {

                    possibleTargets.Add(target);

                }

            }

        }

        Transform nextTarget;

        randomNumber = Random.Range(0, possibleTargets.Count - 1);
        nextTarget = possibleTargets.ToArray()[randomNumber];
        closeTargets.Remove(nextTarget);

        return nextTarget;

    }

    private void OnWaveEnd() {

        troopRemovedOnWaveEndEvent.Raise(troopData);
        Destroy(troopData.GameObject);

    }

    private void OnTroopDeath(RunTimeTroopData troop) {

        Transform troopTransform = troop.GameObject.transform;

        HandleEnemyLeavedAttackRadio(troopTransform);

    }

    private void OnTowerDestruction(RunTimeTowerData tower) {

        Transform towerTransform = tower.GameObject.transform;

        HandleEnemyLeavedAttackRadio(towerTransform);

        construcionsList.Remove(towerTransform);

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

    public int TowerPriorityWeight {

        get { return towerPriorityWeight; }

        set { towerPriorityWeight = value; }

    }

    public int HeroPriorityWeight {

        get { return heroPriorityWeight; }

        set { heroPriorityWeight = value; }

    }

    public int TroopPriorityWeight {

        get { return troopPriorityWeight; }

        set { towerPriorityWeight = value; }

    }

}