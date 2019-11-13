using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMovementActions : MonoBehaviour {

    public GameObject iaSlot;

    private Rigidbody2D troopRigidBody2d;
    private RunTimeTroopData troopData;
    private IsometricCharacterAnimator troopAnimations;
    public Vector2 currentPosition, targetPosition, prevTarget, _actualTarget;
    public Vector2 actualTarget { 
        set {
            _actualTarget = value;
        } 
        get {
            return _actualTarget;
        }
    }
    private PathFinding pathFinding;
    public TilemapHandler tilemapHandler;

    private float motionSpeed; //TO:DO get data from RunTimeData when initialized

    public Vector2 movementDirection;

    public Vector2 movement;

    public Vector2 fixedTargetPosition;

    void Start() {

        troopRigidBody2d = GetComponent<Rigidbody2D>();
        troopData = GetComponent<RunTimeTroopData>();
        troopAnimations = GetComponentInChildren<IsometricCharacterAnimator>();

        actualTarget = transform.position;
        prevTarget = transform.position;

    }

    void Awake() {
        pathFinding = GetComponent<PathFinding>();
        tilemapHandler = GameObject.Find("TilemapHandler").GetComponent<TilemapHandler>();
    }

    private void FixedUpdate() {
        currentPosition = transform.position;
        /*Debug.Log("ActualTarget: " + actualTarget);
        Debug.Log("PrevTarget: " + prevTarget);*/
        if(Mathf.RoundToInt(actualTarget.x) != Mathf.RoundToInt(prevTarget.x)
           || Mathf.RoundToInt(actualTarget.y) != Mathf.RoundToInt(prevTarget.y)) {
            pathFinding.startPos = tilemapHandler.PositionToTilemapNode(currentPosition).position;
            pathFinding.goalPos = tilemapHandler.PositionToTilemapNode(actualTarget).position;
            pathFinding.FindPath();
            prevTarget = actualTarget;
            /*foreach (Vector2 position in pathFinding.path) {

                Instantiate(iaSlot, position, Quaternion.identity);

            }*/
            targetPosition = pathFinding.path.Pop();
            targetPosition.y += 1.5f;
        }

        /*Debug.Log("Movement current position: " + currentPosition.x + " " + currentPosition.y);
        Debug.Log("Movement Target position: " + targetPosition.x + " " + targetPosition.y);*/

        if (Mathf.RoundToInt(currentPosition.x) != Mathf.RoundToInt(targetPosition.x) 
            || Mathf.RoundToInt(currentPosition.y) != Mathf.RoundToInt(targetPosition.y)) {
            MoveToTargetPosition();
        }
        else {
            //Debug.Log("a");
            if(pathFinding.path != null && pathFinding.path.Count > 0) {
                targetPosition = pathFinding.path.Pop();
                targetPosition.y += 1.5f;

                /*Debug.Log("Print Path");
                foreach (Vector2 position in pathFinding.path) {

                    Debug.Log(position);

                }*/

                //Debug.Log(targetPosition);
            }
        }
    }

    public void MoveToTargetPosition() {
        Vector2 roundedTarget = new Vector2(Mathf.RoundToInt(targetPosition.x), Mathf.RoundToInt(targetPosition.y));
        Vector2 roundedCurrent = new Vector2(Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y));
        movementDirection = (roundedTarget - roundedCurrent).normalized;
        movementDirection.x = Mathf.Round(movementDirection.x);
        movementDirection.y = Mathf.Round(movementDirection.y);

        movement = movementDirection * troopData.valMotionSpeed;
        fixedTargetPosition = currentPosition + movement * Time.fixedDeltaTime;

        troopRigidBody2d.MovePosition(fixedTargetPosition);
        troopAnimations.AnimateRun(targetPosition);

    }

    public void WaitOnActualPosition() {

        troopAnimations.AnimateStatic();

    }

}
