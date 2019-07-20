using UnityEngine;

public class PathColisionIgnore : MonoBehaviour {

    void Start() {

        Physics2D.IgnoreLayerCollision(8, 9);

    }

}
