using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour {
    public GoldReserve playerGoldReserve {get; private set;}
    public GoldReserve BobAIGoldReserve { get; private set; }
    
    public IntEvent goldChangeEvent;
    public TroopEvent troopDeathEvent;
    public TroopEvent removedTroopOnWaveEnd;

    private RunTimePhaseData phaseData;

    private void Awake() {

        phaseData = GetComponent<RunTimePhaseData>();

        playerGoldReserve = new PlayerGoldReserve(phaseData.valIniPlayerMoney, troopDeathEvent, removedTroopOnWaveEnd, goldChangeEvent);
        BobAIGoldReserve = new AIGoldReserve(phaseData.valIniIAMoney, troopDeathEvent, removedTroopOnWaveEnd);

    }
}
