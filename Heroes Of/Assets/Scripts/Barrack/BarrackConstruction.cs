using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BarrackConstruction : MonoBehaviour {

    public GameObject barrackPrefab;
    public List<Sprite> barrackSprites;
    public GoldIncrementerTest goldIncrementerTest;

    public SlotService slotService;

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

                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 clickWorldPosition = new Vector2(worldPosition.x, worldPosition.y);

                if(Physics2D.OverlapPoint(clickWorldPosition) != null
                && tilemapHandler.PositionToTilemapNode(Input.mousePosition).tile.isSlot) {
                    alertManager.ShowWarningModal("Slot bloqueado pela torre!");
                } else {
                    BuildBarrack(Input.mousePosition, goldIncrementerTest.playerGoldReserve);
                }

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

                slotService.BlockSlot(slotService.GetSlotTilesFromTile(clickedCell));

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
}
