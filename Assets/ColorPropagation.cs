using UnityEngine;

public class ColorPropagation : MonoBehaviour
{
    private Renderer objectRenderer;

    private Color currentColor;
    private bool hasColor = false;

    void Awake()
    {
        objectRenderer = GetComponentInChildren<Renderer>();

        if (objectRenderer != null)
        {
            currentColor = objectRenderer.material.color;
        }
    }

    public void ApplyRandomColor()
    {
        Color newColor = Random.ColorHSV();

        ApplyColor(newColor);
    }

    public void ApplyColor(Color newColor)
    {
        if (objectRenderer == null)
            return;

        currentColor = newColor;
        hasColor = true;

        objectRenderer.material.color = newColor;
    }

    public bool HasColor()
    {
        return hasColor;
    }

    public Color GetColor()
    {
        return currentColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Try the object directly first
        ColorPropagation other =
            collision.gameObject.GetComponent<ColorPropagation>();

        // If the collider is on a child object,
        // search the parent too.
        if (other == null)
        {
            other =
                collision.gameObject.GetComponentInParent<ColorPropagation>();
        }

        if (other == null)
            return;

        // If THIS object has a color,
        // give that color to the object it hit.
        if (hasColor)
        {
            other.ApplyColor(currentColor);
        }

        // Otherwise, if the OTHER object has a color,
        // receive its color.
        else if (other.hasColor)
        {
            ApplyColor(other.currentColor);
        }
    }
}