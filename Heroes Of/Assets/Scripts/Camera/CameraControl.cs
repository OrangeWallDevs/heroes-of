using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float dragSpeed = 0.6f;
    public float minDragDistance = 0.5f;

    public float zoomSpeed = 0.05f;
    public float zoomOutMin = 3f;
    public float zoomOutMax = 10f;

    public float leftLimit, rightLimit, topLimit, bottomLimit;

    public Transform hero;

    private Vector2 lastTouch, actualTouch;
    private Vector3 cameraPosition;
    private RaycastHit2D hit;
    private Camera cam;

    private void Start() {

        cameraPosition = transform.position;

        cam = Camera.main;

        if (dragSpeed == 0)
            dragSpeed = 0.6f;

        if (zoomSpeed == 0)
            zoomSpeed = 0.05f;

        hit = Physics2D.Raycast(Vector2.zero, Vector2.zero);

    }

    private void Update() {

        if (Input.touchCount <= 1) {

            HandleMovement();

        } 
        else if (Input.touchCount == 2) {

            HandleTouchZoom();

        }

        if (Input.mouseScrollDelta.y != 0)
            HandleMouseZoom();

    }
    
    private void HandleMovement() {

        if (Input.GetMouseButtonDown(0)) {

            lastTouch = Input.mousePosition;

            Vector2 pick = cam.ScreenToWorldPoint(lastTouch);
            hit = Physics2D.Raycast(pick, Vector2.zero);

        } 
        else if (Input.GetMouseButton(0) && hit.collider == null) {

            actualTouch = Input.mousePosition;

            Vector2 deltaTouchPosition = actualTouch - lastTouch;

            if (Mathf.Abs(deltaTouchPosition.x) >= minDragDistance || Mathf.Abs(deltaTouchPosition.y) >= minDragDistance) {

                float cameraStep = dragSpeed * Time.deltaTime;

                cameraPosition = transform.position;

                cameraPosition.x -= deltaTouchPosition.x * cameraStep;
                cameraPosition.y -= deltaTouchPosition.y * cameraStep;

                MoveCamera(cameraPosition);

            }

            lastTouch = actualTouch;

        }

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

    private void MoveCamera(Vector3 direction) {

        // Limits the X and Y that the camera can move
        direction.x = Mathf.Clamp(direction.x, leftLimit, rightLimit);
        direction.y = Mathf.Clamp(direction.y, bottomLimit, topLimit);

        transform.position = direction;

    }

    private void Zoom(float increment) {

        float zoomValue = cam.orthographicSize - increment;

        cam.orthographicSize = Mathf.Clamp(zoomValue, zoomOutMin, zoomOutMax);

    }

    public void FocusHero() {

        MoveCamera(new Vector3(hero.position.x, hero.position.y, transform.position.z));

    }

}
