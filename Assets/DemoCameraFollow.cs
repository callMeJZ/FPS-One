using UnityEngine;

public class DemoCameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Position")]
    public Vector3 offset = new Vector3(0f, 2.5f, -4f);

    [Header("Follow Settings")]
    public float followSpeed = 8f;
    public float rotationSpeed = 8f;

    [Header("Look At")]
    public float lookHeight = 1.2f;

    void LateUpdate()
    {
        if (target == null)
            return;

        // Desired position behind the player.
        Vector3 desiredPosition =
            target.TransformPoint(offset);

        // Smoothly follow the player.
        transform.position =
            Vector3.Lerp(
                transform.position,
                desiredPosition,
                followSpeed * Time.deltaTime
            );

        // Look toward the player's upper body.
        Vector3 lookPosition =
            target.position +
            Vector3.up * lookHeight;

        Vector3 direction =
            lookPosition - transform.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
        }
    }
}