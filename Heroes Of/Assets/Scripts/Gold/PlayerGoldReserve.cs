﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoldReserve : GoldReserve {
    public IntEvent playerGoldChangeEvent;
    public PlayerGoldReserve(int initialGold, GameEvent minionDeadEvent, IntEvent goldChangeEvent) : base(initialGold, minionDeadEvent) {
        playerGoldChangeEvent = goldChangeEvent;
        playerGoldChangeEvent.Raise(initialGold);
    }
    public override void AddGold(GameObject obj) {
        /*TODO: Verificar se obj é minion inimigo e adicionar ouro correspondente

        if(!obj.isEnemy) {
            currentGold += obj.GoldValue; 
        } (algo assim)
        */

        playerGoldChangeEvent.Raise(currentGold);
    }

    public new void AddGold(int goldToAdd) {
        currentGold += goldToAdd;
        playerGoldChangeEvent.Raise(currentGold);
    }

    public override bool SpendGold(int goldAmountToSpend) {
        if(currentGold >= goldAmountToSpend) {
            currentGold -= goldAmountToSpend;

            playerGoldChangeEvent.Raise(currentGold);
            return true;
        }
        
        return false;
    }
}