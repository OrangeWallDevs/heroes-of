using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float dragSpeed = 1f;
    public float minDragDistance = 5.5f;

    public float zoomOutMin = 3f;
    public float zoomOutMax = 10f;
    public float zoomSpeed = 0.05f;

    public float leftLimit, rightLimit, topLimit, bottomLimit;
    public Transform hero;

    private Vector3 cameraPosition;
    private RaycastHit2D hit;
    private Camera cam;
    private Vector3 touchStart;

    private void Start() {

        cameraPosition = transform.position;

        cam = Camera.main;

        if (dragSpeed == 0)
            dragSpeed = 1f;

        if (zoomSpeed == 0)
            zoomSpeed = 0.05f;

        hit = Physics2D.Raycast(Vector2.zero, Vector2.zero);

    }

    private void Update() {

        if (Input.touchCount == 2) { // Zoom

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 initialPositionTouchZero = touchZero.position - touchZero.deltaPosition; // Posição do 1° dedo no 1° toque
            Vector2 initialPositionTouchOne = touchOne.position - touchOne.deltaPosition; // Posição do 2° dedo no 1° toque

            float initialDistance = (initialPositionTouchZero - initialPositionTouchOne).magnitude; // Distância entre dedos
            float actualDistance = (touchZero.position - touchOne.position).magnitude;

            Zoom((initialDistance - actualDistance) * -zoomSpeed);

        } 
        else if (Input.touchCount == 1) { 

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {

                Vector2 pick = cam.ScreenToWorldPoint(touch.position);
                hit = Physics2D.Raycast(pick, Vector2.zero);
                touchStart = cam.ScreenToWorldPoint(touch.position);

            }

            if (hit.collider == null) {

                if (touch.phase == TouchPhase.Moved && (Mathf.Abs(touch.deltaPosition.x) >= minDragDistance || Mathf.Abs(touch.deltaPosition.y) >= minDragDistance)) { // Mover

                    /*Vector3 direction = touchStart - cam.ScreenToWorldPoint(touch.position);
                    transform.position += (direction * dragSpeed * Time.deltaTime); change the drag to 10 or higher speed to test*/


                    cameraPosition = transform.position;

                    float cameraStep = dragSpeed * Time.deltaTime;

                    cameraPosition.x -= touch.deltaPosition.x * cameraStep;
                    cameraPosition.y -= touch.deltaPosition.y * cameraStep;

                    MoveCamera(cameraPosition);

                }

            }

        }

    }

    public void FocusHero() {

        MoveCamera(new Vector3(hero.position.x, hero.position.y, transform.position.z));

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

}
