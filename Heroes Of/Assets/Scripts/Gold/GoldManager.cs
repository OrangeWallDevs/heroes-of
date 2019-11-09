using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour {
    public GoldReserve playerGoldReserve {get; private set;}
    public GoldReserve BobAIGoldReserve { get; private set; }
    
    public IntEvent goldChangeEvent;
    public TroopEvent troopDeathEvent;
    public TroopEvent removedTroopOnWaveEnd;

    void OnEnable() {
        playerGoldReserve = new PlayerGoldReserve(2500, troopDeathEvent, removedTroopOnWaveEnd, goldChangeEvent);
        BobAIGoldReserve = new AIGoldReserve(500, troopDeathEvent, removedTroopOnWaveEnd);
    }
}
