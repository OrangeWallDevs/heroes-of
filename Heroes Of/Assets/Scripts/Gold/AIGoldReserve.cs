using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoldReserve : GoldReserve {
    public override void AddGold(GameObject obj) {
        /*TODO: Verificar se obj é minion inimigo e adicionar ouro correspondente

        if(obj.isEnemy) {
            currentGold += obj.GoldValue; 
        } (algo assim)
        */

        ChangeCurrentGoldAmount.Raise(currentGold);
    }
}
