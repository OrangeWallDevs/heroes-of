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

    private void Start() {

    }

    public void SelectBuildPosition(int barrackID) {

        isPlaceSelected = false;
        this.barrackID = barrackID;
        StartCoroutine(BuildPositionDetection());

    }

    private IEnumerator BuildPositionDetection() {

        while (!isPlaceSelected) {

            if (Input.GetMouseButtonDown(0)) {

                isPlaceSelected = true;

                Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                BuildBarrack(clickPosition, goldIncrementerTest.playerGoldReserve);

                yield break;

            } 
            else {

                yield return new WaitForFixedUpdate();

            }

        }

    }

    private Barrack BuildBarrack(Vector2 position, GoldReserve goldReserve) {

        GameObject barrackGameObject = Instantiate(barrackPrefab, position, new Quaternion());

        Barrack barrack = LoadBarrackData(barrackGameObject);

        if(goldReserve.SpendGold(barrack.VlrCost)) {
            foreach (Sprite sprite in barrackSprites) {

                if (sprite.name == barrack.NamBarrack) {

                    barrack.GameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                    break;

                }

            }

            return barrack;
            
        } else {
            alertManager.ShowWarningModal("Você não tem dinheiro suficiente para comprar essa caserna!");
            Destroy(barrackGameObject);
        }

        return null;   

    }

    private Barrack LoadBarrackData(GameObject gameObject) {

        Barrack barrack = new Barrack(gameObject);

        switch (barrackID) {

            case 1:
                barrack.NamBarrack = "Assassin_Barrack";
                barrack.DesBarrack = "Spawn a bunch of Assassins";
                barrack.VlrSpawnFrequency = 1f;
                barrack.NumTroopLimit = 15;
                barrack.VlrCost = 25;
                break;

            case 2:
                barrack.NamBarrack = "Hunter_Barrack";
                barrack.DesBarrack = "Spawn a bunch of Hunters";
                barrack.VlrSpawnFrequency = 1f;
                barrack.NumTroopLimit = 15;
                barrack.VlrCost = 25;
                break;

            case 3:
                barrack.NamBarrack = "Knight_Barrack";
                barrack.DesBarrack = "Spawn a bunch of Knights";
                barrack.VlrSpawnFrequency = 1f;
                barrack.NumTroopLimit = 15;
                barrack.VlrCost = 25;
                break;

            case 4:
                barrack.NamBarrack = "Mage_Barrack";
                barrack.DesBarrack = "Spawn a bunch of Mages";
                barrack.VlrSpawnFrequency = 1f;
                barrack.NumTroopLimit = 15;
                barrack.VlrCost = 25;
                break;

            case 5:
                barrack.NamBarrack = "Soldier_Barrack";
                barrack.DesBarrack = "Spawn a bunch of Soldiers";
                barrack.VlrSpawnFrequency = 1f;
                barrack.NumTroopLimit = 15;
                barrack.VlrCost = 25;
                break;

        }

        return barrack;

    }

}
