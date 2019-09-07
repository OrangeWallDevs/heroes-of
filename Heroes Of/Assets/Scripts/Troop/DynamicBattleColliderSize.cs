using UnityEngine;

public class DynamicBattleColliderSize : MonoBehaviour {

    public float detectionIncreasedDistance = 0f;

    private RunTimeTroopData troopData;
    private CircleCollider2D troopBattleCollider;

    void Start() {

        troopBattleCollider = GetComponent<CircleCollider2D>();
        troopData = GetComponentInParent<RunTimeTroopData>();

        troopBattleCollider.radius = (troopData.attackDistance + detectionIncreasedDistance) / 10;
        
    }

}
