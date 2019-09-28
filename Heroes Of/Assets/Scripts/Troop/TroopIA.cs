using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

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
    private List<Transform> attackingTowerTroops;

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
        attackingTowerTroops = new List<Transform>();

        troopData = GetComponent<RunTimeTroopData>();
        movementAction = GetComponent<TroopMovementActions>();
        attackAction = GetComponent<TroopAttackActions>();

        troopDeathEvent.RegisterListener(OnTroopDeath);
        towerDestroyedEvent.RegisterListener(OnTowerDestruction);
        attackingTowerEvent.RegisterListener(OnTroopAttackingTower);
        heroDeathEvent.RegisterListener(OnHeroDeath);

    }

    private void Update() {

        if (actualTarget == null) {

            actualTarget = PickAnTarget();

        } else {

        }

        if (attackAction.IsAttacking && actualState == TroopStates.ATTACKING) {

            attackingTowerEvent.Raise(troopData);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        Transform detectedObject = collision.gameObject.transform.parent;
        float detectedObjectPriority = 0;

        if (detectedObject != null) {

            // TO:DO --> Find a better way to set the priority weight
            switch (detectedObject.tag) {

                case ("Tower"):

                    detectedObjectPriority = towerPriorityWeight;
                    break;

                case ("Hero"):

                    detectedObjectPriority = heroPriorityWeight;
                    break;

                case ("Troop"):

                    detectedObjectPriority = troopPriorityWeight;
                    break;

            }

            if (detectedObjectPriority > actualTargetPriority) {

                closeTargets.Add(actualTarget);
                attackAction.StopAttack();
                actualTarget = detectedObject;

            } else {

                closeTargets.Add(detectedObject);

            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision) {

        Transform objectOutOfRadio = collision.gameObject.transform.parent;

        if (objectOutOfRadio != null) {

            if (closeTargets.Contains(objectOutOfRadio)) {

                closeTargets.Remove(objectOutOfRadio);

            }

            if (actualTarget == objectOutOfRadio) {

                if (attackAction.IsAttacking) {

                    attackAction.StopAttack();

                }

                actualTarget = null;

            }

        }

    }

    private Transform PickAnTarget() {

        return null;

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

    private void HandleEnemyDeath(Transform enemy) {

        if (closeTargets.Contains(enemy)) {

            closeTargets.Remove(enemy);

        }

        if (actualTarget == enemy) {

            attackAction.StopAttack();
            actualTarget = null;

        }

    }

    private void OnTroopDeath(RunTimeTroopData troop) {

        Transform troopTransform = troop.GameObject.transform;

        HandleEnemyDeath(troopTransform);

        /*RunTimeTroopData[] enemysTroopsDetectedCopy = enemysTroopsDetected.ToArray();

        foreach (RunTimeTroopData detectedTroop in enemysTroopsDetectedCopy) {

            if (troop == detectedTroop) {

                enemysTroopsDetected.Remove(detectedTroop);

                if (actualAttackingTarget == troop.GameObject) {

                    attackAction.StopAttack();
                    actualAction = null;

                }

            }

        }*/

    }

    private void OnTowerDestruction(RunTimeTowerData tower) {

        Transform towerTransform = tower.GameObject.transform;

        HandleEnemyDeath(towerTransform);

        towersList.Remove(towerTransform);

        /*if (detectedEnemyTower == tower) {

            detectedEnemyTower = null;

            if (actualAttackingTarget == tower.GameObject) {

                attackAction.StopAttack();
                actualAction = null;

            }

        }

        towersList.Remove(tower);*/

    }

    private void OnHeroDeath(RunTimeHeroData hero) {

        Transform heroTransform = hero.GameObject.transform;

        HandleEnemyDeath(heroTransform);

        /*if (actualAttackingTarget == hero.GameObject) {

            attackAction.StopAttack();
            actualAction = null;

        }

        if (detectedEnemyHero == hero) {

            detectedEnemyHero = null;

        }*/

    }

    private void OnTroopAttackingTower(RunTimeTroopData troop) {

        /*if (troopData.troopObjective == PhaseObjectives.DEFEND && actualState == TroopStates.DEFENDING) {

            if (!enemysTroopsDetected.Contains(troop)) {

                enemysTroopsDetected.Add(troop);

            }

        }
        */
    }

}