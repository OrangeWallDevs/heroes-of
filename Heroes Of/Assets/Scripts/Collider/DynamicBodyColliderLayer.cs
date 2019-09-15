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

                Debug.Log("TO:DO: Create a hero RunTimeData");
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
