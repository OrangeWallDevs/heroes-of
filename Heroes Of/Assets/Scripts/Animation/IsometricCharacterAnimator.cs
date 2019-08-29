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

    public void AnimateStatic() {

        PlayAnimation("Static " + lastDirection);

    }

    public void AnimateAttack(Vector2 attackingPosition) {

        PlayAnimationBasedOnDirection("Attack", attackingPosition);

    }

    public void AnimateRun(Vector2 targetPosition) {

        PlayAnimationBasedOnDirection("Run", targetPosition);

    }

    public void PlayAnimation(string animation) {

        lastDirection = animation.Split()[1];
        animator.Play(animation);

    }

    private void PlayAnimationBasedOnDirection(string animation, Vector2 animationDirection) {

        animation += " ";
        lastDirection = "";

        lastDirection = GetDirection(character.position, animationDirection);
        animation += lastDirection;

        animator.Play(animation);

    }

    private string GetDirection(Vector2 characterPosition, Vector2 eventPosition){

        string direction = "";

        // Check which vertical direction the character have to take
        if (characterPosition.y > eventPosition.y) { // Down

            direction += "S";

        } else if (characterPosition.y < eventPosition.y) { // Up

            direction += "N";

        } else { // No vertical movement

            direction += "";

        }

        // Check which horizontal direction the character have to take
        if (characterPosition.x > eventPosition.x) { // Left

            direction += "W";

        } else if (characterPosition.x < eventPosition.x) { // Right

            direction += "E";

        } else { // No horizontal movement

            direction += "";

        }

        return direction;

    }

}
