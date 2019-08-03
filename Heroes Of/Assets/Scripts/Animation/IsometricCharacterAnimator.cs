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

    public void SetDirection(Vector2 rawTargetPosition) {

        string animation = "";

        Vector2 actualPoint = character.position;

        // Check if the Character is moving
        if (Mathf.Ceil(rawTargetPosition.x) != Mathf.Ceil(actualPoint.x)
            || Mathf.Ceil(rawTargetPosition.y) != Mathf.Ceil(actualPoint.y)) { // isMoving

            animation += "Run ";
            lastDirection = "";

            // Check which vertical direction it have to take
            if (actualPoint.y > rawTargetPosition.y) { // Down

                animation += "S";

            } 
            else if (actualPoint.y < rawTargetPosition.y) { // Up

                animation += "N";

            } 
            else { // No vertical movement

                animation += "";

            }

            // Check which horizontal direction it have to take
            if (actualPoint.x > rawTargetPosition.x) { // Left

                animation += "W";

            } 
            else if (actualPoint.x < rawTargetPosition.x) { // Right

                animation += "E";

            } 
            else { // No horizontal movement

                animation += "";

            }

            char[] animationSplit = animation.ToCharArray();
            lastDirection = ("" + animationSplit[animationSplit.Length - 2] + animationSplit[animationSplit.Length - 1]);

        }
        else {

            animation += "Static " + lastDirection;

        }

        animator.Play(animation);

    }

}
