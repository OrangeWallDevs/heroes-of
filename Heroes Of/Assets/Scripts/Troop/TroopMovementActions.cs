using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMovementActions : MonoBehaviour {

    private Rigidbody2D troopRigidBody2d;
    private RunTimeTroopData troopData;
    private IsometricCharacterAnimator troopAnimations;
    private Vector2 currentPosition, targetPosition, prevTarget, _actualTarget;
    public Vector2 actualTarget { 
        set {
            prevTarget = value;
            _actualTarget = value;
        } 
        get {
            return _actualTarget;
        }
    }
    private PathFinding pathFinding;
    public TilemapHandler tilemapHandler;

    private float motionSpeed; //TO:DO get data from RunTimeData when initialized

    void Start() {

        troopRigidBody2d = GetComponent<Rigidbody2D>();
        troopData = GetComponent<RunTimeTroopData>();
        troopAnimations = GetComponentInChildren<IsometricCharacterAnimator>();

    }

    void Awake() {
        pathFinding = GetComponent<PathFinding>();
        tilemapHandler = GameObject.Find("TilemapHandler").GetComponent<TilemapHandler>();
    }

    private void FixedUpdate() {
        currentPosition = transform.position;
        if(actualTarget != prevTarget) {
            pathFinding.startPos = tilemapHandler.PositionToTilemapNode(currentPosition).position;
            pathFinding.goalPos = tilemapHandler.PositionToTilemapNode(actualTarget).position;
            pathFinding.FindPath();
        }
        else if (Mathf.Ceil(currentPosition.x) != Mathf.Ceil(targetPosition.x) 
            || Mathf.Ceil(currentPosition.y) != Mathf.Ceil(targetPosition.y)) {
            MoveToTargetPosition();
        }
        else {
            if(pathFinding.path != null && pathFinding.path.Count > 0) {
                targetPosition = pathFinding.path.Pop();
            }
        }
    }

    public void MoveToTargetPosition() {

        Vector2 actualPosition = transform.position;

        Vector2 movementDirection = (targetPosition - actualPosition).normalized;
        movementDirection.x = Mathf.Round(movementDirection.x);
        movementDirection.y = Mathf.Round(movementDirection.y);

        Vector2 movement = movementDirection * troopData.valMotionSpeed;
        Vector2 fixedTargetPosition = actualPosition + movement * Time.fixedDeltaTime;

        troopRigidBody2d.MovePosition(fixedTargetPosition);
        troopAnimations.AnimateRun(targetPosition);

    }

    public void WaitOnActualPosition() {

        troopAnimations.AnimateStatic();

    }

}
