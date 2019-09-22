using UnityEngine;

public class PathColisionIgnore : MonoBehaviour {

    void Start() {

        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(9, 11);
        Physics2D.IgnoreLayerCollision(9, 12);

    }

}
