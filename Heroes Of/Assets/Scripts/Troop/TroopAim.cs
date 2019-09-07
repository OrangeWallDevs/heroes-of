using System.Text.RegularExpressions;
using UnityEngine;

public class TroopAim : MonoBehaviour {

    private IsometricCharacterAnimator characterAnimator;

    void Start() {

        characterAnimator = transform.parent.GetComponentInChildren<IsometricCharacterAnimator>();

    }

    void Update() {

        string facingDirection = characterAnimator.LastDirection;

        Vector3 fixedFacingPosition = transform.localPosition;

        if (facingDirection.ToUpper().Contains("W")) { // Facing left

            if (fixedFacingPosition.x > 0) {

                fixedFacingPosition.x = -1 * fixedFacingPosition.x;

            }

        }
        else if (facingDirection.ToUpper().Contains("E")) { // Facing right

            fixedFacingPosition.x = Mathf.Abs(fixedFacingPosition.x);

        }

        transform.localPosition = fixedFacingPosition;

    }

}
