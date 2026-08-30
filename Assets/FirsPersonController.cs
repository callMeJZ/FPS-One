using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 2.0f;

    [Header("References")]
    public Animator animator;

    private CharacterController controller;
    private Transform cameraTransform;

    private float xRotation = 0f;

    void Start()
    {
        controller =
            GetComponent<CharacterController>();

        cameraTransform =
            Camera.main.transform;

        // Automatically find the Animator
        // on the player model if not assigned.
        if (animator == null)
        {
            animator =
                GetComponentInChildren<Animator>();
        }

        Cursor.lockState =
            CursorLockMode.Locked;

        Cursor.visible = false;
    }

    void Update()
    {
        HandleLook();
        HandleMovement();
        UpdateAnimation();
    }

    void HandleLook()
    {
        float mouseX =
            Input.GetAxis("Mouse X")
            * mouseSensitivity;

        float mouseY =
            Input.GetAxis("Mouse Y")
            * mouseSensitivity;

        xRotation -= mouseY;

        xRotation =
            Mathf.Clamp(
                xRotation,
                -90f,
                90f
            );

        cameraTransform.localRotation =
            Quaternion.Euler(
                xRotation,
                0f,
                0f
            );

        transform.Rotate(
            Vector3.up * mouseX
        );
    }

    void HandleMovement()
    {
        float horizontal =
            Input.GetAxis("Horizontal");

        float vertical =
            Input.GetAxis("Vertical");

        bool isRunning =
            Input.GetKey(KeyCode.LeftShift);

        float currentSpeed =
            isRunning
                ? runSpeed
                : walkSpeed;

        Vector3 move =
            transform.right * horizontal +
            transform.forward * vertical;

        // Prevent diagonal movement from being faster.
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        controller.Move(
            move *
            currentSpeed *
            Time.deltaTime
        );
    }

    void UpdateAnimation()
    {
        if (animator == null)
            return;

        float horizontal =
            Input.GetAxis("Horizontal");

        float vertical =
            Input.GetAxis("Vertical");

        Vector2 input =
            new Vector2(
                horizontal,
                vertical
            );

        float inputMagnitude =
            Mathf.Clamp01(
                input.magnitude
            );

        bool isRunning =
            Input.GetKey(KeyCode.LeftShift);

        float targetBlend = 0f;

        if (inputMagnitude > 0.01f)
        {
            targetBlend =
                isRunning ? 6f : 3f;
        }

        animator.SetFloat(
            "Blend",
            targetBlend,
            0.1f,
            Time.deltaTime
        );
    }
}