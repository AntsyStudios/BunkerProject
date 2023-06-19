using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed            = 10.0f;
    public float Gravity          = -9.8f;
    public float MouseSensitivity = 10.0f;
    public float MousePanLimit    = 45.0f;

    private float mRotationX = 0.0f;

    public Transform CameraTransform;
    private CharacterController mCharacterController;

    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();

        // Lock and hide cursor

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    void Update()
    {
        MousePan();
        Move();
    }

    private void MousePan()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        mRotationX -= mouseY;
        mRotationX  = Mathf.Clamp(mRotationX, -MousePanLimit, MousePanLimit);

        CameraTransform.localRotation = Quaternion.Euler(mRotationX, 0.0f, 0.0f);
        transform.rotation *= Quaternion.Euler(0.0f, mouseX, 0.0f);
    }

    private void Move()
    {
        // Move with WASD

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right   = transform.TransformDirection(Vector3.right);

        float speedX = Speed * Input.GetAxis("Vertical");
        float speedY = Speed * Input.GetAxis("Horizontal");
        
        Vector3 moveDirection = forward * speedX + right * speedY;

        // Gravity

        moveDirection.y += Gravity * Time.deltaTime;

        // Move CharacterController

        mCharacterController.Move(moveDirection * Time.deltaTime);
    }
}
