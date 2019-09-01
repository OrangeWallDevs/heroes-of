using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObject/GoldIncrementTest")]
public class GoldIncrementerTest : ScriptableObject {
    public PlayerGoldReserve playerGoldReserve {get; private set;}
    public IntEvent goldChangeEvent;
    void OnEnable() {
        playerGoldReserve = new PlayerGoldReserve(500, null, goldChangeEvent);
        playerGoldReserve.SpendGold(200);
    }
}
