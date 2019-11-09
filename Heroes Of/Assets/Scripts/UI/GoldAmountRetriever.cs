using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldAmountRetriever : MonoBehaviour {
    public IntEvent playerGoldChangeEvent;

    private TextMeshProUGUI coinDisplay;
    private GoldManager goldManager; //TODO: Replace this for "GameRuntimeData" SO

    private void Awake() {
        GameObject phaseManager = GameObject.FindGameObjectWithTag("Phase_Manager");
        goldManager = phaseManager.GetComponent<GoldManager>();
    }

    public void Start() {
        coinDisplay = GetComponent<TextMeshProUGUI>();
        playerGoldChangeEvent.RegisterListener(UpdateCoinDisplay);
        UpdateCoinDisplay(goldManager.playerGoldReserve.currentGold);
    }

    private void UpdateCoinDisplay(int newGoldAmount) {
        coinDisplay.text = newGoldAmount.ToString();
    }
}
