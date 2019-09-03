using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GoldReserve {
    public int currentGold {get; protected set;}
    public GameEvent deadMinionEvent; //TODO: Replace this GameEvent for GameEvent<Minion> or something like that
    public GoldReserve(int initialGold, GameEvent minionDeadEvent) {
        currentGold = initialGold;
        deadMinionEvent = minionDeadEvent;
        //deadMinionEvent.RegisterListener(AddGold);
    }

    public abstract void AddGold(GameObject obj);

    public void AddGold(int goldToAdd) {
        currentGold += goldToAdd;
    }

    public abstract bool SpendGold(int goldAmountToSpend);
}
