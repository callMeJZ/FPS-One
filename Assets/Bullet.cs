using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 30f;
    public float lifetime = 5f;

    [Header("Damage")]
    public float damage = 25f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Destroy the bullet automatically after its lifetime
        Destroy(gameObject, lifetime);

        // Give the bullet its forward velocity
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Send damage to the object that was hit,
        // including its parent objects.
        //
        // The future TrainingDummy script will contain:
        //
        // public void TakeDamage(float damage)
        //
        // "DontRequireReceiver" prevents errors if the
        // object being hit does not have TakeDamage().
        collision.collider.SendMessageUpwards(
            "TakeDamage",
            damage,
            SendMessageOptions.DontRequireReceiver
        );

        // Destroy the bullet after hitting something
        Destroy(gameObject);
    }
}