using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackFactory : MonoBehaviour {

    public BarrackScriptableObject[] barracksSO;
    public GameObject barrackPrefab;

    // Start is called before the first frame update
    void Start() {

    }

    public Barrack CreateBarrack(int codBarrack, bool isEnemy, PhaseObjectives barrackObjective) {

        GameObject barrackGameObject = GameObject.Instantiate(barrackPrefab);
        Barrack barrack = new Barrack(barrackGameObject);
        BarrackScriptableObject barrackData = GetBarrackScriptableObject(codBarrack);

        barrack.CodBarrack = barrackData.codBarrack;
        barrack.CodPart = barrackData.codPart;
        barrack.CodTroop = barrackData.codTroop;
        barrack.NamBarrack = barrackData.namBarrack;
        barrack.DesBarrack = barrackData.desBarrack;
        barrack.NumTroopLimit = barrackData.numTroopLimit;
        barrack.VlrCost = barrackData.vlrCost;
        barrack.VlrSpawnFrequency = barrackData.vlrSpawnFrequency;
        barrack.IsEnemy = isEnemy;
        barrack.Objective = barrackObjective;

        barrack.GameObject.GetComponent<SpriteRenderer>().sprite = barrackData.sprite;
        //barrack.GameObject.GetComponent<Animator>().runtimeAnimatorController = barrackData.animatorController;

        barrack.GameObject.GetComponent<RunTimeBarrackData>().SetData(barrack);

        return barrack;

    }

    private BarrackScriptableObject GetBarrackScriptableObject(int barrackCod) {

        foreach (BarrackScriptableObject barrackSO in barracksSO) {

            if (barrackSO.codBarrack == barrackCod) {

                return barrackSO;

            }

        }

        return null;

    }

}
