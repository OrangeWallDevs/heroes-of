using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMovementActions : MonoBehaviour {

    private Rigidbody2D troopRigidBody2d;
    private RunTimeTroopData troopData;
    private IsometricCharacterAnimator troopAnimations;
    private Vector2 currentPosition, targetPosition;
    private PathFinding pathFinding;


    void Start() {

        troopRigidBody2d = GetComponent<Rigidbody2D>();
        troopData = GetComponent<RunTimeTroopData>();
        troopAnimations = GetComponentInChildren<IsometricCharacterAnimator>();

    }

    void Awake() {
        pathFinding = GetComponent<PathFinding>();
        currentPosition = transform.position;
    }

    public void MoveToPosition(Vector2 targetPosition) {

        Vector2 actualPosition = transform.position;

        Vector2 movementDirection = (targetPosition - actualPosition).normalized;
        movementDirection = Vector2.ClampMagnitude(movementDirection, 1);

        Vector2 movement = movementDirection * troopData.vlrMotionSpeed;
        Vector2 fixedTargetPosition = actualPosition + movement * Time.fixedDeltaTime;

        troopRigidBody2d.MovePosition(fixedTargetPosition);
        troopAnimations.AnimateRun(targetPosition);

    }

    public void WaitOnActualPosition() {

        troopAnimations.AnimateStatic();

    }

}
