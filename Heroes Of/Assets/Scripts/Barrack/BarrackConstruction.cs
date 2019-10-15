using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        AvaliableTileSelection.ChangeMask(tilemapHandler,true);

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
        Node clickedCell = tilemapHandler.PositionToTilemapNode(position);

        if(clickedCell.tile.isSlot && clickedCell.isAvailable) {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(position);

            Barrack barrack = barrackFactory.CreateBarrack(barrackID, buildForEnemy, buildForObjective);

            if(goldReserve.SpendGold(barrack.ValCost)) {
                barrack.GameObject.transform.position = new Vector3(clickPosition.x, clickPosition.y, 0);

                BlockSlot(clickedCell);
                AvaliableTileSelection.ChangeMask(tilemapHandler,false);
                return barrack;
            } else {
                alertManager.ShowWarningModal("Você não tem dinheiro suficiente para comprar essa caserna!");
                Destroy(barrack.GameObject);
            }
        } else {
            AvaliableTileSelection.ChangeMask(tilemapHandler,false);
            alertManager.ShowWarningModal("Esse slot está bloqueado!");
        }

        return null;   

    }

    private void BlockSlot(Node slotTile) {
        const int slotRange = 6;
        List<Node> slotTilesNeighbours = tilemapHandler.GetAllTileNeighbours(slotTile, slotRange - 1)
            .Where(node => node.tile.isSlot).ToList();
        
        foreach(Node slotNeighbour in slotTilesNeighbours) {
            slotNeighbour.isAvailable = false;
        }
    }

}
