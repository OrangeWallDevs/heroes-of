using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldAmountRetriever : MonoBehaviour {
    private TextMeshProUGUI coinDisplay;
    public IntEvent playerGoldChangeEvent;
    public GoldIncrementerTest goldIncrementerTest; //TODO: Replace this for "GameRuntimeData" SO

    public void Start() {
        coinDisplay = GetComponent<TextMeshProUGUI>();
        playerGoldChangeEvent.RegisterListener(UpdateCoinDisplay);
        UpdateCoinDisplay(goldIncrementerTest.playerGoldReserve.currentGold);
    }

    private void UpdateCoinDisplay(int newGoldAmount) {
        coinDisplay.text = newGoldAmount.ToString();
    }
}
