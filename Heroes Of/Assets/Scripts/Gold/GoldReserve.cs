using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoldReserve {
    public int currentGold {get; private set;}
    public GameEvent<int> ChangeCurrentGoldAmount;
    public GoldReserve(int initialGold) {
        currentGold = initialGold;
    }

    public abstract void AddGold(GameObject obj);

    public bool SpendGold(int goldAmountToSpend) {
        if(currentGold >= goldAmountToSpend) {
            currentGold -= goldAmountToSpend;

            ChangeCurrentGoldAmount.Raise(currentGold);
            return true;
        }
        
        return false;
    }
}
