using UnityEngine;

public class RunTimeTroopData : MonoBehaviour {

    public string namTroop;
    public int vlrDamageDealt;
    public int vlrHp;
    public int vlrScore;
    public int vlrDropMoney;

    public float vlrMotionSpeed;
    public float vlrAttackSpeed;
    public float attackDistance;
    public bool isEnemy;
    public bool attackAtDistance;

    public GameObject GameObject {
        get { return gameObject; }
    }

}
