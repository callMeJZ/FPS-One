using UnityEngine;

public class ColorPropagation : MonoBehaviour
{
    private Renderer objectRenderer;

    private Color currentColor;

    private bool hasColor = false;

    void Awake()
    {
        objectRenderer =
            GetComponentInChildren<Renderer>();

        if (objectRenderer != null)
        {
            currentColor =
                objectRenderer.material.color;
        }
    }

    public void ApplyColor(Color newColor)
    {
        if (objectRenderer == null)
            return;

        currentColor = newColor;

        hasColor = true;

        objectRenderer.material.color =
            currentColor;
    }

    public bool HasColor()
    {
        return hasColor;
    }

    public Color GetColor()
    {
        return currentColor;
    }

    void OnCollisionEnter(Collision collision)
    {
        ColorPropagation other =
            collision.gameObject
                .GetComponentInParent<ColorPropagation>();

        if (other == null)
            return;

        // This object is colored.
        // Pass its color to the object it hit.
        if (hasColor)
        {
            other.ApplyColor(currentColor);
        }

        // Otherwise receive the color
        // from the object we collided with.
        else if (other.hasColor)
        {
            ApplyColor(other.currentColor);
        }
    }
}