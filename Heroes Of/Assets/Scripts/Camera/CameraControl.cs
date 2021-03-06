using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour {

    public float dragSpeed = 0.8f;
    public float minDragDistance = 1f;

    public float zoomSpeed = 0.05f;
    public float zoomOutMin = 3f, zoomOutMax = 10f;

    public float leftLimit, rightLimit, topLimit, bottomLimit;

    public Transform hero;

    private int fingerTouchID;
    private Vector2 lastTouch, actualTouch;
    private Vector3 cameraPosition;
    private Camera cam;

    private void Start() {

        cameraPosition = transform.position;

        cam = Camera.main;

        if (dragSpeed == 0)
            dragSpeed = 0.8f;

        if (zoomSpeed == 0)
            zoomSpeed = 0.05f;

    }

    private void Update() {
        fingerTouchID = -1;

        if(Input.touchCount > 0) {
            fingerTouchID = Input.GetTouch(0).fingerId;
        }

        if(EventSystem.current.IsPointerOverGameObject(fingerTouchID)) 
            return;

        if (Input.touchCount <= 1) {

            HandleMovement();

        } 
        else if (Input.touchCount == 2) {

            HandleTouchZoom();

        }

        if (Input.mouseScrollDelta.y != 0) {

            HandleMouseZoom();

        }

    }

    private void HandleMovement() {

        if (Input.GetMouseButtonDown(0)) {

            lastTouch = Input.mousePosition;

        } 
        else if (Input.GetMouseButton(0)) {

            actualTouch = Input.mousePosition;

            Vector2 deltaTouchPosition = actualTouch - lastTouch;

            if (Mathf.Abs(deltaTouchPosition.x) >= minDragDistance || Mathf.Abs(deltaTouchPosition.y) >= minDragDistance) {

                cameraPosition = transform.position;

                cameraPosition.x -= deltaTouchPosition.x;
                cameraPosition.y -= deltaTouchPosition.y;

                MoveCamera(cameraPosition);

            }

            lastTouch = actualTouch;

        }

    }

    private void MoveCamera(Vector3 direction) {

        // Limits the X and Y that the camera can move 
        direction.x = Mathf.Clamp(direction.x, leftLimit, rightLimit);
        direction.y = Mathf.Clamp(direction.y, bottomLimit, topLimit);

        float cameraStep = Time.deltaTime * dragSpeed;

        transform.position = Vector3.Lerp(transform.position, direction, cameraStep);

    }

    private void HandleTouchZoom() {

        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 initialPositionTouchZero = touchZero.position - touchZero.deltaPosition; // Position of finger 1 on 1° touch 
        Vector2 initialPositionTouchOne = touchOne.position - touchOne.deltaPosition; // Position of finger 2 on 1° touch 

        float initialDistance = (initialPositionTouchZero - initialPositionTouchOne).magnitude; // Distence between 2 fingers 
        float actualDistance = (touchZero.position - touchOne.position).magnitude;

        Zoom((initialDistance - actualDistance) * -zoomSpeed);

    }

    private void HandleMouseZoom() {

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Zoom(zoomSpeed * scroll * 100f);

    }

    private void Zoom(float increment) {

        float zoomValue = cam.orthographicSize - increment;

        cam.orthographicSize = Mathf.Clamp(zoomValue, zoomOutMin, zoomOutMax);

    }

    public void FocusHero() {

        StartCoroutine(MoveTawardsHero());

    }

    private IEnumerator MoveTawardsHero() {

        Vector3 heroPosition = new Vector3(hero.position.x, hero.position.y, transform.position.z);

        while (Mathf.Abs(heroPosition.x - transform.position.x) >= 0.5 ||
            Mathf.Abs(heroPosition.y - transform.position.y) >= 0.5) {

            if (Input.GetMouseButtonDown(0)) {

                yield break;

            }

            heroPosition = new Vector3(hero.position.x, hero.position.y, transform.position.z);

            MoveCamera(heroPosition);
            yield return new WaitForFixedUpdate();

        }

    }

}
