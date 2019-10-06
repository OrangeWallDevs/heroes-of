using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeBarrackData : MonoBehaviour {

    public int codBarrack;
    public int codPart;
    public int codTroop;
    public string namBarrack;
    public string desBarrack;
    public float vlrSpawnFrequency;
    public int vlrCost;
    public int numTroopLimit;
    public bool isEnemy;
    public PhaseObjectives objective;

    public void SetData(Barrack barrackData) {

        codBarrack = barrackData.CodBarrack;
        codPart = barrackData.CodPart;
        codTroop = barrackData.CodTroop;
        namBarrack = barrackData.NamBarrack;
        desBarrack = barrackData.DesBarrack;
        vlrSpawnFrequency = barrackData.VlrSpawnFrequency;
        vlrCost = barrackData.VlrCost;
        numTroopLimit = barrackData.NumTroopLimit;
        isEnemy = barrackData.IsEnemy;
        objective = barrackData.Objective;

    }

}
