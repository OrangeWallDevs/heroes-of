using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hero_Movement : MonoBehaviour {

    public BoolEvent heroSelectEvent;
    public ObjectEvent placeSelectEvent;

    private PositionAndState heroPositionAndState;
    public float movementSpeed = 5f;

    private Rigidbody2D heroRigidbody;
    private CapsuleCollider2D heroColider;
    private Transform heroTransform;

    private IsometricCharacterAnimator characterAnimatorScript;

    private Vector2 currentPosition, targetPosition;

    private RaycastHit2D hit;
    private bool isHeroSelected, isCoroutineAllowedSelection;
    private float firstClickTime, timeBetweenClicks;
    private int clickCounter;

    private bool clickDown, clickUp;
    private float timeBetweenDownToUp, clickDownTime;
    private Vector2 clickDownPosition, clickUpPosition;
    private PathFinding pathFinding;

    [SerializeField]
    private TilemapHandler tilemapHandler;

    private void Start() {

        firstClickTime = 0f;
        timeBetweenClicks = 0.2f;
        clickCounter = 0;
        isCoroutineAllowedSelection = true;

        clickDown = false;
        clickUp = false;
        clickDownTime = 0f;
        timeBetweenDownToUp = 0.5f;

        hit = Physics2D.Raycast(Vector2.zero, Vector2.zero);
    }

    private void Awake() {

        characterAnimatorScript = GetComponentInChildren<IsometricCharacterAnimator>();
        heroColider = GetComponentInChildren<CapsuleCollider2D>();

        heroRigidbody = GetComponent<Rigidbody2D>();
        heroTransform = GetComponent<Transform>();

        pathFinding = GetComponent<PathFinding>();

        targetPosition = heroTransform.position;

        heroPositionAndState = new PositionAndState(targetPosition,false);

    }

    private void Update() {

        if (Input.touchCount <= 1) {

            HandleHeroSelection();

            HandleMovement();

        }

    }

    private void FixedUpdate() {

        currentPosition = heroTransform.position;

        if (Mathf.Ceil(currentPosition.x) != Mathf.Ceil(targetPosition.x) 
            || Mathf.Ceil(currentPosition.y) != Mathf.Ceil(targetPosition.y)) {
                
            Vector2 directionMovement = (targetPosition - currentPosition).normalized;

            directionMovement.x = Mathf.Round(directionMovement.x);
            directionMovement.y = Mathf.Round(directionMovement.y);
            
            Vector2 movement = directionMovement * movementSpeed;
            Vector2 newPosition = currentPosition + movement * Time.fixedDeltaTime;

            heroRigidbody.MovePosition(newPosition);
            characterAnimatorScript.AnimateRun(targetPosition);

        }
        else {
            if(pathFinding.path != null && pathFinding.path.Count > 0) {
                targetPosition = pathFinding.path.Pop();
            }
            characterAnimatorScript.AnimateStatic();

            heroPositionAndState.Position = targetPosition;
            heroPositionAndState.IsAtPosition = true;

            placeSelectEvent.Raise(heroPositionAndState);
        }

    }

    private void HandleHeroSelection() {

        if (Input.GetMouseButtonDown(0)) {

            hit = DetectHit(Input.mousePosition);

        }
        else if (Input.GetMouseButtonUp(0)) {

            if (hit.collider == null && isHeroSelected) { // Action to unselect

                clickCounter++;

                if (clickCounter == 1 && isCoroutineAllowedSelection) {

                    firstClickTime = Time.time;
                    StartCoroutine(HeroUnselection());

                }

            }
            else if (hit.collider == heroColider) { // Action to select

                isHeroSelected = true;
                heroSelectEvent.Raise(isHeroSelected);
                Debug.Log("Hero Selected!");

            }

        }

    }

    private void HandleMovement() {

        if (Input.GetMouseButtonDown(0) && isHeroSelected) {

            clickDownPosition = Input.mousePosition;
            hit = DetectHit(clickDownPosition);

            clickDown = true;
            clickDownTime = Time.time;

            StartCoroutine(RestartClick());

        }
        else if (Input.GetMouseButtonUp(0) && isHeroSelected && clickDown) {

            clickUpPosition = Input.mousePosition;
            clickUp = true;

            StopCoroutine(RestartClick());

        }

        if (clickDown && clickUp && clickDownPosition == clickUpPosition) {

            Vector2 clickedPos = Input.mousePosition;
            if(tilemapHandler.IsTile(Camera.main.ScreenToWorldPoint(clickedPos))) {
                Vector3Int gridPos = tilemapHandler.ScreenToCellPosition(clickedPos);
                Node clickedCell = tilemapHandler.GetTile(gridPos);
                if(clickedCell.tile.isWalkable) {
                    pathFinding.startPos = tilemapHandler.WorldToCellPosition(currentPosition);
                    pathFinding.goalPos = clickedCell.position;
                    pathFinding.FindPath();

                    heroPositionAndState.Position = targetPosition;
                    heroPositionAndState.IsAtPosition = false;
                    placeSelectEvent.Raise(heroPositionAndState);
                }
            }

            clickUp = false;
            clickDown = false;
            clickDownTime = 0f;

        }

    }

    private RaycastHit2D DetectHit(Vector2 eventPosition) {

        Camera camera = Camera.main;

        Vector2 pick = camera.ScreenToWorldPoint(eventPosition);

        return Physics2D.Raycast(pick, Vector2.zero);

    }


    private IEnumerator HeroUnselection() {

        isCoroutineAllowedSelection = false;

        while (Time.time <= timeBetweenClicks + firstClickTime) {

            if (clickCounter >= 2) {

                isHeroSelected = false;
                heroSelectEvent.Raise(isHeroSelected);
                Debug.Log("Hero Unselected :(");
                break;

            }

            yield return new WaitForEndOfFrame();

        }

        clickCounter = 0;
        firstClickTime = 0f;
        isCoroutineAllowedSelection = true;

    }

    private IEnumerator RestartClick() {

        while (Time.time <= timeBetweenDownToUp + clickDownTime) {

            if (clickUp) {
                break;
            }

            yield return new WaitForFixedUpdate();

        }

        if (!clickUp) {

            clickUp = false;
            clickDown = false;
            clickDownTime = 0f;

        }

        yield break;

    }

}
