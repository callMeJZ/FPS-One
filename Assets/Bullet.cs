using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 30f;
    public float lifetime = 5f;

    private Rigidbody rb;
    private Renderer bulletRenderer;

    // The color currently carried by this bullet
    private Color bulletColor = Color.white;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        bulletRenderer =
            GetComponentInChildren<Renderer>();
    }

    void Start()
    {
        // Automatically destroy after some time
        Destroy(gameObject, lifetime);

        // Move the bullet forward
        if (rb != null)
        {
            rb.linearVelocity =
                transform.forward * speed;
        }
    }

    // Called by GunMechanics when the bullet is created
    public void SetColor(Color newColor)
    {
        bulletColor = newColor;

        if (bulletRenderer != null)
        {
            bulletRenderer.material.color =
                bulletColor;
        }
    }

    public Color GetColor()
    {
        return bulletColor;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Find a ColorPropagation component
        // on the object that was hit or its parent.
        ColorPropagation target =
            collision.gameObject
                .GetComponentInParent<ColorPropagation>();

        if (target != null)
        {
            // Transfer THIS bullet's color.
            target.ApplyColor(bulletColor);
        }

        // Destroy the bullet after impact.
        Destroy(gameObject);
    }
}