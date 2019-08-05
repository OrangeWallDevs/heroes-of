using UnityEngine;

public class EnemyRemoveTest : MonoBehaviour {
    
    void Update() {
    
        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject);

    }

}
