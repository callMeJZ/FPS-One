using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 30f;
    public float lifetime = 5f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Destroy the bullet after a few seconds
        Destroy(gameObject, lifetime);

        // Move the bullet forward
        rb.linearVelocity = transform.forward * speed;
    }
}