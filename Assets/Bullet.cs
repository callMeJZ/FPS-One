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
    ObjectiveTarget objective =
        collision.gameObject
            .GetComponentInParent<ObjectiveTarget>();

    if (objective != null)
    {
        ColorPropagation colorTarget =
            collision.gameObject
                .GetComponentInParent<ColorPropagation>();

        if (colorTarget != null)
        {
            colorTarget.ApplyColor(bulletColor);
        }

        LevelObjectives manager =
            FindFirstObjectByType<LevelObjectives>();

        if (manager != null)
        {
            Color requiredColor =
                objective.GetRequiredColor();

            if (!Approximately(
                bulletColor,
                requiredColor))
            {
                manager.RegisterIncorrectObject();
            }
        }

        Destroy(gameObject);
        return;
    }

    ColorPropagation target =
        collision.gameObject
            .GetComponentInParent<ColorPropagation>();

    if (target != null)
    {
        target.ApplyColor(bulletColor);
    }

    Destroy(gameObject);
}

bool Approximately(Color a, Color b)
{
    return Mathf.Abs(a.r - b.r) < 0.05f &&
           Mathf.Abs(a.g - b.g) < 0.05f &&
           Mathf.Abs(a.b - b.b) < 0.05f;
}
}