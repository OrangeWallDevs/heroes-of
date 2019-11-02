using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoldReserve : GoldReserve {
    
    public IntEvent playerGoldChangeEvent;

    public PlayerGoldReserve(int initialGold, TroopEvent minionDeadEvent, TroopEvent removedMinionEvent, 
        IntEvent goldChangeEvent) : base(initialGold, minionDeadEvent, removedMinionEvent) {
        playerGoldChangeEvent = goldChangeEvent;
        playerGoldChangeEvent.Raise(initialGold);
    }

    public override void AddGold(RunTimeTroopData troopData) {
        if (troopData.isEnemy) {
            AddGold(troopData.valDropMoney);
            playerGoldChangeEvent.Raise(currentGold);
        }
    }

    public override void AddGoldOnWaveEnd(RunTimeTroopData troopData) {
        if (!troopData.isEnemy) {
            AddGold(troopData.valDropMoney);
        }
    }

    public new void AddGold(int goldToAdd) {
        currentGold += goldToAdd;
        playerGoldChangeEvent.Raise(currentGold);
    }

    public override bool SpendGold(int goldAmountToSpend) {
        if (currentGold >= goldAmountToSpend) {
            currentGold -= goldAmountToSpend;

            playerGoldChangeEvent.Raise(currentGold);
            return true;
        }

        return false;
    }
}