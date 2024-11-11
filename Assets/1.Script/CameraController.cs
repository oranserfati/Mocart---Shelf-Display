using UnityEngine;

public enum CameraState
{
    Default,
    Zooming,
    Rotating,
    Reseting
}

public class CameraController : MonoBehaviour
{
    public CameraState cameraState = CameraState.Default;
    public Transform target;
    public float zoomDistance = 5f;
    public float zoomTime = 1f;
    public float rotationSpeed = 5f;          
    public float damping = 0.9f;              

    private float dTime = 0;
    private Vector3 offset;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 zoomedPosition;
    private Quaternion zoomedRotation;
    private Vector3 rotationVelocity;

    public bool isRotating = false;

    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float zoomSmoothTime = 0.2f;

    private float currentZoomDistance;
    private float targetZoomDistance;
    private float zoomVelocity;

    void Start()
    {
        UserInput.onProductClick += StartZoomToTarget;
        ResetViewButton.onClickReset += StartReset;

        // Store the camera's initial position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void OnDestroy()
    {
        UserInput.onProductClick -= StartZoomToTarget;
        ResetViewButton.onClickReset -= StartReset;
    }

    void Update()
    {
        if (cameraState == CameraState.Zooming)
        {
            ZoomToTarget(target.position);
        }
        else if (cameraState == CameraState.Reseting)
        {
            ResetCameraPosition();
        }
        else if (cameraState == CameraState.Rotating)
        {
            RotateAroundTarget();
            HandleZoom();
        }
    }

    private void StartZoomToTarget(Transform t)
    {
        if (cameraState != CameraState.Rotating)
        {
            target = t;
            offset = (transform.position - target.position).normalized * zoomDistance;
            currentZoomDistance = zoomDistance;
            targetZoomDistance = zoomDistance;
            dTime = 0;
            cameraState = CameraState.Zooming;
        }
    }

    private void StartReset(Transform t)
    {
        zoomedPosition = transform.position;
        zoomedRotation = transform.rotation;
        target = null;
        dTime = 0;
        cameraState = CameraState.Reseting;
    }

    private void ZoomToTarget(Vector3 t)
    {
        dTime += Time.deltaTime;

        offset = (transform.position - t).normalized * zoomDistance;
        Vector3 targetPosition = t + offset;

        // Smoothly move towards target position
        transform.position = Vector3.Slerp(initialPosition, targetPosition, dTime / zoomTime);

        if (dTime >= zoomTime)
        {
            dTime = 0;
            cameraState = CameraState.Rotating;
        }
    }

    public void ResetCameraPosition()
    {
        dTime += Time.deltaTime;

        // Smoothly move and rotate back to the initial view
        transform.position = Vector3.Slerp(zoomedPosition, initialPosition, dTime / zoomTime);
        transform.rotation = Quaternion.Slerp(zoomedRotation, initialRotation, dTime / zoomTime);

        if (dTime >= zoomTime)
        {
            dTime = 0;
            currentZoomDistance = maxZoom;
            cameraState = CameraState.Default;
        }
    }

    private void RotateAroundTarget()
    {
        if (Input.GetMouseButton(0))
        {
            // Capture mouse input to update rotation velocity
            float horizontalInput = Input.GetAxis("Mouse X");
            float verticalInput = Input.GetAxis("Mouse Y");

            rotationVelocity.x = -verticalInput * rotationSpeed;
            rotationVelocity.y = horizontalInput * rotationSpeed;
        }
        else
        {
            // Smooth stop
            rotationVelocity *= damping;
        }

        // Apply rotation around target
        Quaternion rotationHorizontal = Quaternion.AngleAxis(rotationVelocity.y * Time.deltaTime, Vector3.up);
        Quaternion rotationVertical = Quaternion.AngleAxis(rotationVelocity.x * Time.deltaTime, transform.right);

        offset = rotationHorizontal * rotationVertical * offset;

        // Update camera position and orientation
        transform.position = target.position + offset.normalized * currentZoomDistance;
        transform.LookAt(target);
    }

    private void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scrollInput) > Mathf.Epsilon)
        {
            // Adjust target zoom based on scroll input
            targetZoomDistance -= scrollInput * rotationSpeed;
            targetZoomDistance = Mathf.Clamp(targetZoomDistance, minZoom - 1f, maxZoom + 1f);
        }

        // Smooth zoom transition
        currentZoomDistance = Mathf.SmoothDamp(currentZoomDistance, Mathf.Clamp(targetZoomDistance, minZoom, maxZoom), ref zoomVelocity, zoomSmoothTime);

        // Update position based on zoom
        transform.position = target.position + offset.normalized * currentZoomDistance;
    }
}
