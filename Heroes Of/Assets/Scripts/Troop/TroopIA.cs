﻿using System.Collections;
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

    private PriorityList<Transform> construcionsList;

    private RunTimeTroopData troopData;
    private TroopMovementActions movementAction;
    private TroopAttackActions attackAction;

    private PathFinding pathFinding;

    private void Awake() {

        construcionsList = new PriorityList<Transform>();

        GameObject[] constructionsInGame = GameObject.FindGameObjectsWithTag("Tower");
        Vector2 troopPosition = transform.position;


        foreach (GameObject construction in constructionsInGame) {

            Vector2 constructionPosition = construction.transform.position;
            int distance = (int) Vector2.Distance(troopPosition, constructionPosition);

            PriorityPair<Transform> pair = new PriorityPair<Transform>(construction.transform, distance);

            construcionsList.Add(pair);

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

            nextTarget = FindNextTargetBuilding();

        }
        else if (closeTargets.Count > 0){

            PriorityList<string> tagsPriority = new PriorityList<string>();

            foreach (Transform target in closeTargets) {

                if (!tagsPriority.ContainsKey(target.tag)) {

                    PriorityPair<string> pair = new PriorityPair<string>(target.tag, GetTargetPriority(target));
                    tagsPriority.Add(pair);

                }

            }

            int randomNumber = Random.Range(0, troopPriorityWeight + towerPriorityWeight + heroPriorityWeight);
            int lastPriority = 0;
            string selectedTag = "";

            for (int i = tagsPriority.Count - 1; i >= 0 ; i--) {

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

                if (target.tag == selectedTag) {

                    possibleTargets.Add(target);

                }

            }

            randomNumber = Random.Range(0, possibleTargets.Count - 1);
            nextTarget = possibleTargets.ToArray()[randomNumber];
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

    private Transform FindNextTargetBuilding() {

        Transform targetTower;

        if (troopData.troopObjective == PhaseObjectives.ATTACK) { // Get the closest tower as target

            targetTower = construcionsList.GetLast().Key;

        }
        else { // Get the most distant tower as target

            targetTower = construcionsList.GetFirst().Key;

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

}