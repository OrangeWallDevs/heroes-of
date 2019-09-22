using UnityEngine;

public class DynamicBodyColliderLayer : MonoBehaviour {

    private bool isEnemy;

    void Start() {

        switch (transform.parent.tag) {

            case ("Troop"):

                RunTimeTroopData troopData = GetComponentInParent<RunTimeTroopData>();
                isEnemy = troopData.isEnemy;
                break;

            case ("Tower"):

                RunTimeTowerData towerData = GetComponentInParent<RunTimeTowerData>();
                isEnemy = towerData.isEnemy;
                break;

            case ("Hero"):

                RunTimeHeroData heroData = GetComponentInParent<RunTimeHeroData>();
                isEnemy = heroData.isEnemy;
                break;

        }

        if (isEnemy) {

            gameObject.layer = 11;

        }
        else {

            gameObject.layer = 12;

        }
        
    }

}
