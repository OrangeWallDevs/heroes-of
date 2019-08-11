using UnityEngine;

public class EnemyRemoveTest : MonoBehaviour {
    
    void Update() {
    
        if (Input.GetMouseButtonDown(1))
            Destroy(gameObject);

    }

}
