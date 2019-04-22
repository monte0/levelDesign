using UnityEngine;

public class CameraController: MonoBehaviour
{
    private const float MIN_DISTANCE = 1.0f;
    private const float MAX_DISTANCE = 10.0f;

    public Transform target;

    private Transform camTransform;

    private Camera cam;

    public float distance;

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    public Transform pivot;

    private Vector3 dollyDir;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;

        dollyDir = camTransform.position.normalized;
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        Vector3 desiredCameraPos = target.TransformPoint(dollyDir * MAX_DISTANCE);

        RaycastHit hit;

        if (Physics.Linecast(target.position, desiredCameraPos, out hit))
            distance = Mathf.Clamp((hit.distance * 0.9f), MIN_DISTANCE, MAX_DISTANCE);
        else
            distance = MAX_DISTANCE;
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = target.position + rotation * dir;

        target.Rotate(0, Input.GetAxis("Mouse X"), 0);
        pivot.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);

        if (camTransform.position.y < target.position.y)
            camTransform.position = new Vector3(camTransform.position.x, target.position.y - 0.5f, camTransform.position.z);

        //if (camTransform.position.z == target.position.z)
            //camTransform.position = new Vector3(camTransform.position.x, camTransform.position.y, target.position.z);

        camTransform.LookAt(target.position);
    }
}
