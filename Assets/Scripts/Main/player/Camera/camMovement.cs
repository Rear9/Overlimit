using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camMovement : MonoBehaviour
{
    public static float sensX, sensY, slowSens;
    public Camera cam;
    public Transform orientation;
    public Wallrun Wallrun;
    public Climb climb;
    public Slowmo slowmo;
    public static float fov;
    private float xRot, yRot, baseFov = 75f, maxFov = 130f;
    public Rigidbody rb;
    public float mouseX, mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        slowSens = (1-slowmo.slowFinal) * 10;
    }

    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        if (slowmo.slow)
        {
            mouseX *= slowSens;
            mouseY *= slowSens;
        }
        fov = PlayerPrefs.GetFloat("Fov");
        sensX = PlayerPrefs.GetFloat("Sens");
        sensY = PlayerPrefs.GetFloat("Sens");
    }

    private void FixedUpdate()
    {
        float addFov = rb.velocity.magnitude - 3f;
        if (climb.climbing)
        {
            addFov = rb.velocity.magnitude - 2f;
        }
        fov = Mathf.Lerp(fov, baseFov + addFov, .25f);
        fov = Mathf.Clamp(fov, baseFov, maxFov);

    }

    private void LateUpdate()
    {
        cam.fieldOfView = fov;
        RotateCam();
    }
    void RotateCam()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        if (slowmo.slow)
        {
            mouseX *= slowSens;
            mouseY *= slowSens;
        }
        yRot += mouseX;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRot, yRot, Wallrun.Tilt);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
