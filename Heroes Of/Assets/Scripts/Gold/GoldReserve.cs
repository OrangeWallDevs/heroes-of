using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GoldReserve {
    public int currentGold { get; protected set; }

    public TroopEvent deadMinionEvent;
    
    public GoldReserve(int initialGold, TroopEvent minionDeadEvent) {
        currentGold = initialGold;
        deadMinionEvent = minionDeadEvent;
        deadMinionEvent.RegisterListener(AddGold);
    }

    public abstract void AddGold(RunTimeTroopData troopData);

    public void AddGold(int goldToAdd) {
        currentGold += goldToAdd;
    }

    public abstract bool SpendGold(int goldAmountToSpend);
}