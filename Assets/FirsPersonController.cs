using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    [Header("Jump")]
    public float jumpHeight = 1.5f;
    public float gravity = -20f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 2f;

    [Header("References")]
    public Transform playerCamera;
    public Animator animator;

    private CharacterController controller;

    private float xRotation = 0f;
    private float verticalVelocity = -2f;

    void Start()
    {
        controller =
            GetComponent<CharacterController>();

        // Automatically find the camera under Player
        // if it has not been assigned manually.
        if (playerCamera == null)
        {
            Camera childCamera =
                GetComponentInChildren<Camera>();

            if (childCamera != null)
            {
                playerCamera =
                    childCamera.transform;
            }
        }

        // Automatically find Animator on the model.
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
        HandleJump();
        ApplyGravity();
        UpdateAnimation();
    }

    void HandleLook()
    {
        // Do not process FPS camera look
        // while the third-person demo camera is active.
        if (playerCamera == null)
            return;

        Camera fpsCamera =
            playerCamera.GetComponent<Camera>();

        if (fpsCamera == null ||
            !fpsCamera.gameObject.activeInHierarchy)
        {
            return;
        }

        float mouseX =
            Input.GetAxis("Mouse X") *
            mouseSensitivity;

        float mouseY =
            Input.GetAxis("Mouse Y") *
            mouseSensitivity;

        xRotation -= mouseY;

        xRotation =
            Mathf.Clamp(
                xRotation,
                -90f,
                90f
            );

        playerCamera.localRotation =
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
        float x =
            Input.GetAxis("Horizontal");

        float z =
            Input.GetAxis("Vertical");

        Vector3 move =
            transform.right * x +
            transform.forward * z;

        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        bool isRunning =
            Input.GetKey(KeyCode.LeftShift);

        float speed =
            isRunning
                ? runSpeed
                : walkSpeed;

        controller.Move(
            move *
            speed *
            Time.deltaTime
        );
    }

    void HandleJump()
    {
        if (controller.isGrounded &&
            verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) &&
            controller.isGrounded)
        {
            verticalVelocity =
                Mathf.Sqrt(
                    jumpHeight *
                    -2f *
                    gravity
                );
        }
    }

    void ApplyGravity()
    {
        verticalVelocity +=
            gravity *
            Time.deltaTime;

        Vector3 verticalMove =
            Vector3.up *
            verticalVelocity;

        controller.Move(
            verticalMove *
            Time.deltaTime
        );
    }

    void UpdateAnimation()
    {
        if (animator == null)
            return;

        // Jump animation
        if (!controller.isGrounded)
        {
            animator.SetFloat(
                "Blend",
                7f,
                0.05f,
                Time.deltaTime
            );

            return;
        }

        float x =
            Input.GetAxis("Horizontal");

        float z =
            Input.GetAxis("Vertical");

        Vector2 input =
            new Vector2(x, z);

        bool moving =
            input.magnitude > 0.01f;

        bool isRunning =
            Input.GetKey(KeyCode.LeftShift);

        float blend = 0f;

        if (moving)
        {
            blend =
                isRunning
                    ? 6f
                    : 3f;
        }

        animator.SetFloat(
            "Blend",
            blend,
            0.1f,
            Time.deltaTime
        );
    }
}