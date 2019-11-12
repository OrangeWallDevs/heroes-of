using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerCounterHP : MonoBehaviour {

    public TowerEvent towerBeingAttackedEvent;

    private TextMeshProUGUI textCounterHP;
    private RunTimeTowerData towerData;

    private int startHp;

    private void Awake() {

        textCounterHP = GetComponent<TextMeshProUGUI>();
        towerData = GetComponentInParent<RunTimeTowerData>();

        startHp = towerData.valHp;

    }

    private void Start() {

        towerBeingAttackedEvent.RegisterListener(UpdateHPCounter);

        SetCounterValue(towerData.valHp);

    }

    private void OnDestroy() {

        towerBeingAttackedEvent.UnregisterListener(UpdateHPCounter);

    }

    private void UpdateHPCounter(RunTimeTowerData towerAttacked) {

        if (towerData.GameObject.Equals(towerAttacked.GameObject)) {

            SetCounterValue(towerData.valHp);

        }

    }

    private void SetCounterValue(int actualHp) {

        textCounterHP.text = actualHp + "/" + startHp;

    }

}
