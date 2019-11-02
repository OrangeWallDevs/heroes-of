using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GoldReserve {
    public int currentGold { get; protected set; }

    public TroopEvent deadMinionEvent;
    public TroopEvent removedMinionAtWaveEndEvent;
    
    public GoldReserve(int initialGold, TroopEvent minionDeadEvent, TroopEvent removedMinionAtWaveEndEvent) {
        currentGold = initialGold;
        deadMinionEvent = minionDeadEvent;
        deadMinionEvent.RegisterListener(AddGold);
        this.removedMinionAtWaveEndEvent = removedMinionAtWaveEndEvent;
        this.removedMinionAtWaveEndEvent.RegisterListener(AddGoldOnWaveEnd);
    }

    public abstract void AddGold(RunTimeTroopData troopData);

    public abstract void AddGoldOnWaveEnd(RunTimeTroopData troopData);

    public void AddGold(int goldToAdd) {
        currentGold += goldToAdd;
    }

    public abstract bool SpendGold(int goldAmountToSpend);
}