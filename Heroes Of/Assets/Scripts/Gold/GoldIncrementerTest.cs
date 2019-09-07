using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObject/GoldIncrementTest")]
public class GoldIncrementerTest : ScriptableObject {
    public GoldReserve playerGoldReserve {get; private set;}
    
    public IntEvent goldChangeEvent;
    public TroopEvent troopDeathEvent;

    void OnEnable() {
        playerGoldReserve = new PlayerGoldReserve(25, troopDeathEvent, goldChangeEvent);
    }
}
