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
}