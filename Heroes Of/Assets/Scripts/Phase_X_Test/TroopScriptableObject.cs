using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Troop")]
public class TroopScriptableObject : ScriptableObject {

    public int codTroop;
    public string namTroop;
    public int vlrDamageDealt;
    public int vlrHp;
    public int vlrScore;
    public float vlrMorionSpeed;
    public float vlrAttackSpeed;
    public int vlrDropMoney;

    public float vlrAttackDistance;
    public PhaseObjectives objective;
    public bool attackAtDistance;
    public bool isEnemy;

    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
    
}
