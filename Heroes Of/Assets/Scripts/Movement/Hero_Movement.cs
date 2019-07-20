using System.Collections;
using UnityEngine;

public class Hero_Movement : MonoBehaviour {

    public float movementSpeed = 5f;

    private Rigidbody2D heroRigidbody;
    private CircleCollider2D heroColider;
    private Transform heroTransform;

    private bool isHeroSelected;
    private Vector2 currentPosition;
    private Vector2 targetPosition;
    private Vector2 touchIniPosition;

    private float firstClickTime, timeBetweenClicks;
    private bool coroutineAllowed;
    private int clickCounter;

    private void Start() {

        firstClickTime = 0f;
        timeBetweenClicks = 0.5f;
        clickCounter = 0;
        coroutineAllowed = true;

        touchIniPosition = new Vector2();

    }

    private void Awake() {

        heroRigidbody = GetComponent<Rigidbody2D>();
        heroColider = GetComponentInChildren<CircleCollider2D>();
        heroTransform = GetComponent<Transform>();
        targetPosition = heroTransform.position;

    }

    private void Update() {

        Camera camera = Camera.main;

        if (Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);

            Vector2 pick = camera.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(pick, Vector2.zero);

            if (isHeroSelected && hit.collider == null) {

                if (touch.phase == TouchPhase.Ended)
                    clickCounter++;

                if (clickCounter == 1 && coroutineAllowed) {

                    firstClickTime = Time.time;
                    StartCoroutine(WasHeroUnselected());

                }

            } 
            else if (hit.collider != null) {

                if (hit.collider.Equals(heroColider)) {

                    isHeroSelected = true;
                    Debug.Log("Selecionado");

                }

            }

            if (isHeroSelected) {

                if (hit.collider != null && hit.transform.tag == "Path") {

                    if (touch.phase == TouchPhase.Began)
                        touchIniPosition = touch.position;

                    if (touch.phase == TouchPhase.Ended && touchIniPosition == touch.position)
                        targetPosition = camera.ScreenToWorldPoint(touch.position);

                }

            }

        }

    }
    private void FixedUpdate() {

        currentPosition = heroTransform.position;

        if (Mathf.Ceil(currentPosition.x) != Mathf.Ceil(targetPosition.x) 
            || Mathf.Ceil(currentPosition.y) != Mathf.Ceil(targetPosition.y)) {

            Vector2 directionMovement = (targetPosition - currentPosition).normalized;
            directionMovement = Vector2.ClampMagnitude(directionMovement, 1);

            Vector2 movement = directionMovement * movementSpeed;

            Vector2 newPosition = currentPosition + movement * Time.fixedDeltaTime;

            heroRigidbody.MovePosition(newPosition);

        }

    }

    private IEnumerator WasHeroUnselected() {

        coroutineAllowed = false;

        while (Time.time <= timeBetweenClicks + firstClickTime) {

            if (clickCounter >= 2) {

                isHeroSelected = false;
                Debug.Log("Deselecionado");
                break;

            }

            yield return new WaitForEndOfFrame();

        }

        clickCounter = 0;
        firstClickTime = 0f;
        coroutineAllowed = true;

    }

}
