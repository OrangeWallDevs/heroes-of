using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoldReserve : GoldReserve {
    public AIGoldReserve(int initialGold, GameEvent minionDeadEvent) : base(initialGold, minionDeadEvent) {}
    public override void AddGold(GameObject obj) {
        /*TODO: Verificar se obj é minion inimigo e adicionar ouro correspondente

        if(obj.isEnemy) {
            currentGold += obj.GoldValue; 
        } (algo assim)
        */
    }

    public override bool SpendGold(int goldAmountToSpend) {
        if(currentGold >= goldAmountToSpend) {
            currentGold -= goldAmountToSpend;
            
            return true;
        }
        
        return false;
    }
}
