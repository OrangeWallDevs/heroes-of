using System.Collections;
using UnityEngine;

public class Hero_Movement : MonoBehaviour {

    public float movementSpeed = 5f;

    private Rigidbody2D heroRigidbody;
    private CircleCollider2D heroColider;
    private Transform heroTransform;

    private IsometricCharacterAnimator characterAnimatorScript;

    private Vector2 currentPosition, targetPosition;

    private RaycastHit2D hit;
    private bool isHeroSelected, isCoroutineAllowed;
    private float firstClickTime, timeBetweenClicks;
    private int clickCounter;

    private void Start() {

        firstClickTime = 0f;
        timeBetweenClicks = 0.2f;
        clickCounter = 0;
        isCoroutineAllowed = true;

    }

    private void Awake() {

        characterAnimatorScript = GetComponentInChildren<IsometricCharacterAnimator>();
        heroColider = GetComponentInChildren<CircleCollider2D>();

        heroRigidbody = GetComponent<Rigidbody2D>();
        heroTransform = GetComponent<Transform>();

        hit = Physics2D.Raycast(Vector2.zero, Vector2.zero);

        targetPosition = heroTransform.position;

    }

    private void Update() {

        if (Input.touchCount <= 1) {

            HandleHeroSelection();

            HandleMovement();

        }

    }

    private void FixedUpdate() {

        currentPosition = heroTransform.position;

        Vector2 directionMovement = (targetPosition - currentPosition).normalized;
        directionMovement = Vector2.ClampMagnitude(directionMovement, 1);

        Vector2 movement = directionMovement * movementSpeed;
        Vector2 newPosition = currentPosition + movement * Time.fixedDeltaTime;

        if (Mathf.Ceil(currentPosition.x) != Mathf.Ceil(targetPosition.x) 
            || Mathf.Ceil(currentPosition.y) != Mathf.Ceil(targetPosition.y)) {

            heroRigidbody.MovePosition(newPosition);

        }

        characterAnimatorScript.SetDirection(targetPosition);

    }

    private void HandleHeroSelection() {

        if (Input.GetMouseButtonDown(0)) {

            hit = DetectHit(Input.mousePosition);

        }
        else if (Input.GetMouseButtonUp(0)) {

            if (hit.collider == null && isHeroSelected) { // Action to unselect

                clickCounter++;

                if (clickCounter == 1 && isCoroutineAllowed) {

                    firstClickTime = Time.time;
                    StartCoroutine(HeroUnselection());

                }

            }
            else if (hit.collider == heroColider) { // Action to select

                isHeroSelected = true;
                Debug.Log("Hero Selected!");

            }

        }

    }

    private void HandleMovement() {

        if (Input.GetMouseButtonDown(0) && isHeroSelected) {

            hit = DetectHit(Input.mousePosition);

            if (hit.collider != null && hit.transform.tag == "Path") {

                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            }

        }

    }

    private RaycastHit2D DetectHit(Vector2 eventPosition) {

        Camera camera = Camera.main;

        Vector2 pick = camera.ScreenToWorldPoint(eventPosition);

        return Physics2D.Raycast(pick, Vector2.zero);

    }

    private IEnumerator HeroUnselection() {

        isCoroutineAllowed = false;

        while (Time.time <= timeBetweenClicks + firstClickTime) {

            if (clickCounter >= 2) {

                isHeroSelected = false;
                Debug.Log("Hero Unselected :(");
                break;

            }

            yield return new WaitForEndOfFrame();

        }

        clickCounter = 0;
        firstClickTime = 0f;
        isCoroutineAllowed = true;

    }

}
