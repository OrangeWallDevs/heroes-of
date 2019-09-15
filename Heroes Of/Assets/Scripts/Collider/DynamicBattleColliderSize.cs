using UnityEngine;

public class DynamicBattleColliderSize : MonoBehaviour {

    public float detectionIncreasedDistance = 0f;

    private float attackDistance;
    private CircleCollider2D battleCollider;

    void Start() {

        switch (transform.parent.tag) {

            case ("Troop"):

                RunTimeTroopData troopData = GetComponentInParent<RunTimeTroopData>();
                attackDistance = troopData.attackDistance;
                break;

            case ("Hero"):

                /*RunTimeHeroData heroData = GetComponentInParent<RunTimeHeroData>();
                attackDistance = heroData.attackDistance;*/
                break;

        }

        battleCollider = GetComponent<CircleCollider2D>();
        battleCollider.radius = (attackDistance + detectionIncreasedDistance) / 10;
        
    }

}
