using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackConstruction : MonoBehaviour {

    public GameObject barrackPrefab;
    public List<Sprite> barrackSprites;
    public GoldIncrementerTest goldIncrementerTest;

    public PopUpManager alertManager;
    private int barrackID;

    private bool isPlaceSelected;

    private Coroutine buildPositionSelection;

    //TO:DO Use RunTimeData to load and create the barrack
    public BarrackFactory barrackFactory;
    public bool buildForEnemy;
    public PhaseObjectives buildForObjective; 

    public TilemapHandler tilemapHandler;
    public void SelectBuildPosition(int barrackID) {

        if (buildPositionSelection != null) {

            StopCoroutine(buildPositionSelection);
            buildPositionSelection = null;

        }

        isPlaceSelected = false;
        this.barrackID = barrackID;
        buildPositionSelection = StartCoroutine(BuildPositionDetection());

    }

    private IEnumerator BuildPositionDetection() {

        while (!isPlaceSelected) {

            if (Input.GetMouseButtonDown(0)) {

                isPlaceSelected = true;

                BuildBarrack(Input.mousePosition, goldIncrementerTest.playerGoldReserve);

                if (buildPositionSelection != null) {

                    StopCoroutine(buildPositionSelection);

                }
                yield break;

            } 
            else {

                yield return new WaitForFixedUpdate();

            }

        }

    }

    private Barrack BuildBarrack(Vector2 position, GoldReserve goldReserve) {

        if(tilemapHandler.PositionToTilemapNode(position).tile.isSlot){
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(position);

            Barrack barrack = barrackFactory.CreateBarrack(barrackID, buildForEnemy, buildForObjective);

            if(goldReserve.SpendGold(barrack.ValCost)) {
                barrack.GameObject.transform.position = new Vector3(clickPosition.x, clickPosition.y, 0);
                return barrack;
            } 
            else {
                alertManager.ShowWarningModal("Você não tem dinheiro suficiente para comprar essa caserna!");
                Destroy(barrack.GameObject);
            }
        } else {
            alertManager.ShowWarningModal("AAAAAAAAAAAAAAAAAAAAAAAAA");
        }

        return null;   

    }

}
