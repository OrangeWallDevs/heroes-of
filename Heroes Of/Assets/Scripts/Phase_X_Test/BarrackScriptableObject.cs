using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Barrack")]
public class BarrackScriptableObject : ScriptableObject {

    public int codBarrack;
    public int codPart;
    public int codTroop;
    public string namBarrack;
    public string desBarrack;
    public float valSpawnFrequency;
    public int valCost;
    public int numTroopLimit;

    public Sprite sprite;
    public RuntimeAnimatorController animatorController;

}
