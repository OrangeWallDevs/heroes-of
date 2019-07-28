using UnityEngine;

public class IsometricCharacterAnimator : MonoBehaviour {

    private Animator animator;
    private Transform character;

    private void Awake() {

        animator = GetComponent<Animator>();
        character = GetComponent<Transform>();

    }

    public void SetDirection(Vector2 targetPosition) {

        string animation = "";

        Vector2 actualPoint = character.position;

        // Check if the Character is moving
        if (actualPoint != targetPosition) { // isMoving

            animation += "Run ";

        }
        else {

            animation += "Static ";

        }

        // Check which vertical direction it have to take
        if (actualPoint.y > targetPosition.y) {

            animation += "S"; // Down

        }
        else if (actualPoint.y < targetPosition.y) {

            animation += "N"; // Up

        }
        else {

            animation += ""; // No vertical movement

        }

        // Check which horizontal direction it have to take
        if (actualPoint.x > targetPosition.x) {

            animation += "W"; // Left

        } else if (actualPoint.x < targetPosition.x) {

            animation += "E"; // Right

        } else {

            animation += ""; // No horizontal movement

        }

        Debug.Log(animation);
        animator.Play(animation);

    }

}
