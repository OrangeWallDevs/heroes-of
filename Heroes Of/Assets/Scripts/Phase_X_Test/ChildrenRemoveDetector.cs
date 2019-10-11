using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenRemoveDetector : MonoBehaviour {

    public GameEvent allTroopsRemoved;

    private void OnTransformChildrenChanged() {
        
        if (transform.childCount == 0) {

            allTroopsRemoved.Raise();

        }

    }

}
