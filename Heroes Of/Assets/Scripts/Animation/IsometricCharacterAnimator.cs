using UnityEngine;

public class IsometricCharacterAnimator : MonoBehaviour {

    private Animator animator;
    private Transform character;

    private string lastDirection;

    private void Awake() {

        animator = GetComponent<Animator>();
        character = GetComponent<Transform>();

        lastDirection = "SW";

    }

    public void SetDirection(Vector2 targetPosition) {

        string animation = "";

        Vector2 actualPoint = character.position;

        // Check if the Character is moving
        if (Mathf.Ceil(targetPosition.x) != Mathf.Ceil(actualPoint.x)
            || Mathf.Ceil(targetPosition.y) != Mathf.Ceil(actualPoint.y)) { // isMoving

            animation += "Run ";
            lastDirection = "";

            // Check which vertical direction it have to take
            if (actualPoint.y > targetPosition.y) {

                animation += "S"; // Down
                lastDirection += "S";

            } else if (actualPoint.y < targetPosition.y) {

                animation += "N"; // Up
                lastDirection += "N";

            } else {

                animation += ""; // No vertical movement
                lastDirection += "";

            }

            // Check which horizontal direction it have to take
            if (actualPoint.x > targetPosition.x) {

                animation += "W"; // Left
                lastDirection += "W";

            } else if (actualPoint.x < targetPosition.x) {

                animation += "E"; // Right
                lastDirection += "E";

            } else {

                animation += ""; // No horizontal movement
                lastDirection += "";

            }

        }
        else {

            animation += "Static " + lastDirection;

        }

        animator.Play(animation);

    }

}
