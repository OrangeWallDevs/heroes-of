using UnityEngine;

public class AISlotUnlocker : MonoBehaviour {

    public GameObject prefabAIBuildSlot;
    public TowerEvent towerDestroyedEvent;

    public bool unlockSlot;

    private void Start() {

        towerDestroyedEvent.RegisterListener(HandleTowerDestroyed);

    }

    private void HandleTowerDestroyed(RunTimeTowerData towerDestroyed) {

        Transform tower = gameObject.transform.parent;

        if (tower == towerDestroyed.GameObject.transform) {

            if (unlockSlot) {

                Instantiate(prefabAIBuildSlot, transform.position, Quaternion.identity);

            }
            towerDestroyedEvent.UnregisterListener(HandleTowerDestroyed);

        }

    }

}
