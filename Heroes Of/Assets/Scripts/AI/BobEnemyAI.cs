using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobEnemyAI : MonoBehaviour {

    public GameEvent waveStartEvent, finalWaveStartEvent;
    public TowerEvent towerDestroyedEvent;

    // TO:DO --> Use the barracks in RuntimeData
    public BarrackScriptableObject[] barracks;
    public BarrackFactory barrackFactory;

    // TO:DO --> Use the instances in RuntimeData
    public GoldIncrementerTest goldIncrementer;

    private PriorityList<BarrackScriptableObject> barracksPriorityList;

    private PriorityList<Transform> constructionsList;
    private List<Vector2> buildingPositions;

    private GoldReserve bobAIGoldReserve;
    private PhaseObjectives bobAIObjective;
    
    private void Awake() {

        GameObject[] positionsGameObjects = GameObject.FindGameObjectsWithTag("AI_Build_Position");
        buildingPositions = new List<Vector2>();

        for (int i = 0; i < positionsGameObjects.Length; i++) {

            buildingPositions.Add(positionsGameObjects[i].transform.position);

        }

        // TO:DO --> This is might be very heavy because a lot of GO do this
        constructionsList = new PriorityList<Transform>();
        GameObject[] towersInPhase = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject tower in towersInPhase) {

            int orderInAttack = tower.GetComponent<TowerAttackOrder>().orderInAttack;
            constructionsList.Add(tower.transform, orderInAttack);

        }

        GameObject core = GameObject.FindGameObjectWithTag("Core");
        int lastTowerOrder = constructionsList.GetFirst().Priority;
        constructionsList.Add(core.transform, lastTowerOrder);

    }

    private void Start() {

        bobAIGoldReserve = goldIncrementer.BobAIGoldReserve;

        // TO:DO --> Use barracks in RuntimeData
        barracksPriorityList = new PriorityList<BarrackScriptableObject>();
        foreach (BarrackScriptableObject barrack in barracks) {

            barracksPriorityList.Add(barrack, barrack.valCost);

        }

        // TO:DO --> Use the phaseData in RuntimeData
        RunTimePhaseData phaseData = GetComponent<RunTimePhaseData>();
        if (phaseData.idtPhaseType == PhaseObjectives.ATTACK) {

            bobAIObjective = PhaseObjectives.DEFEND;

        }
        else {

            bobAIObjective = PhaseObjectives.ATTACK;

        }

        waveStartEvent.RegisterListener(HandleWaveStartEvent);
        towerDestroyedEvent.RegisterListener(HandleTowerDestroyedEvent);

    }

    private void HandleWaveStartEvent() {

        // TO:DO --> Use the money and barracks on RuntimeData
        int lowestBarrackValue = barracksPriorityList.GetLast().Priority;

        while (bobAIGoldReserve.currentGold >= lowestBarrackValue) {

            BarrackScriptableObject barrackToBuild = barracksPriorityList.GetLast().Key;

            for (int i = barracksPriorityList.Count - 1; i >= 0; i--) {

                BarrackScriptableObject compareBarrack = barracksPriorityList.Find(i).Key;

                if (barrackToBuild.valCost <= compareBarrack.valCost) {

                    if (compareBarrack.valCost <= bobAIGoldReserve.currentGold) {

                        barrackToBuild = compareBarrack;

                    }
                    else {

                        break;

                    }

                }

            }

            buildBarrack(barrackToBuild);

        }

    }

    private void buildBarrack(BarrackScriptableObject barrackToBuild) {

        // TO:DO --> Use RuntimeData to create the barrack
        Barrack barrack = barrackFactory.CreateBarrack(barrackToBuild.codBarrack, true, bobAIObjective);

        Vector2 buildPosition = PickBuildPosition();

        barrack.GameObject.transform.position = buildPosition;
        bobAIGoldReserve.SpendGold(barrack.ValCost);

        buildingPositions.Remove(buildPosition);

    }

    private Vector2 PickBuildPosition() {

        Vector2 actualConstructionPosition = constructionsList.GetFirst().Key.position;

        Vector2 buildPosition = buildingPositions[0];
        float lowestDistance = Vector2.Distance(buildPosition, actualConstructionPosition);

        foreach (Vector2 slotPosition in buildingPositions) {

            float compareDistance = Vector2.Distance(slotPosition, actualConstructionPosition);

            if (compareDistance < lowestDistance) {

                lowestDistance = compareDistance;
                buildPosition = slotPosition;

            }

        }

        return buildPosition;

    }

    private void HandleTowerDestroyedEvent(RunTimeTowerData tower) {

        if (!tower.isEnemy) {

            // Add new slot to array of buldingPositions

        }

    }

}
