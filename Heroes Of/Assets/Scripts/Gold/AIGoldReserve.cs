using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoldReserve : GoldReserve {

    public AIGoldReserve(int initialGold, TroopEvent minionDeadEvent) : base(initialGold, minionDeadEvent) { }
    
    public override void AddGold(RunTimeTroopData troopData) {
        if (!troopData.isEnemy) {
            AddGold(troopData.valDropMoney);
        }
    }

    public override bool SpendGold(int goldAmountToSpend) {
        if (currentGold >= goldAmountToSpend) {
            currentGold -= goldAmountToSpend;

            return true;
        }

        return false;
    }
}