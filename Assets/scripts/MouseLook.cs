using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform directions;

    public float mouseSensitivity = 100f;
    public Transform verticalPivot;

    float mouseX, mouseY;

    public static Vector6 camerapoint = new Vector6(0,0,0,0,0,0);

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Vector3.Distance(camerapoint.toVector3(), transform.position) > 0.01f)
        {
            // Yes i know this is not the way it should work but for now it gives the idea
            transform.position = Vector3.Lerp(transform.position, camerapoint.toVector3(), 10f * Time.deltaTime);
        }

        directions.position = camerapoint.toVector3();

        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        verticalPivot.localRotation = Quaternion.Euler(mouseY, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, mouseX, 0f);

        float direction = Input.GetKey(KeyCode.LeftShift) ? -2 : 2;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            camerapoint.y += direction;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            camerapoint.x += direction;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            camerapoint.t += direction;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            camerapoint.u += direction;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            camerapoint.z += direction;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            camerapoint.v += direction;
        }
    }
}