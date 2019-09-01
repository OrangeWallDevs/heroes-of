using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeTroopData : MonoBehaviour {

    public string namTroop;
    public int vlrDamageDealt;
    public int vlrHp;
    public int vlrScore;
    public int vlrMotionSpeed;
    public int vlrDropMoney;
    public float vlrAttackSpeed;

    public bool isEnemy;

    public GameObject GameObject {
        get { return gameObject; }
    }

}
