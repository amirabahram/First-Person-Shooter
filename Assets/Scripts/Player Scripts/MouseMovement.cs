using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon; 
    [SerializeField]
    private Transform playerBody;
    public float mouseSensitivity = 500f;
    float xRotation = 0f;
    float yRotation = 0f;
    public float topClamp = -45f;
    public float downClamp = 45f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, topClamp, downClamp);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
