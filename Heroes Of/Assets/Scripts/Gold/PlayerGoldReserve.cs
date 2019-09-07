using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoldReserve : GoldReserve {
    
    public IntEvent playerGoldChangeEvent;

    public PlayerGoldReserve(int initialGold, TroopEvent minionDeadEvent, IntEvent goldChangeEvent) : base(initialGold, minionDeadEvent) {
        playerGoldChangeEvent = goldChangeEvent;
        playerGoldChangeEvent.Raise(initialGold);
    }

    public override void AddGold(RunTimeTroopData troopData) {
        if (troopData.isEnemy) {
            AddGold(troopData.vlrDropMoney);
            playerGoldChangeEvent.Raise(currentGold);
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