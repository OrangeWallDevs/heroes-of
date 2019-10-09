using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Troop")]
public class TroopScriptableObject : ScriptableObject {

    public int codTroop;
    public string namTroop;
    public int valDamageDealt;
    public int valHp;
    public int valScore;
    public float valMorionSpeed;
    public float valAttackSpeed;
    public int valDropMoney;

    public float valAttackDistance;
    public PhaseObjectives objective;
    public bool attackAtDistance;
    public bool isEnemy;

    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
    public GameObject projectilePrefab;

}
