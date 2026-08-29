using UnityEngine;

public class ObjectiveTarget : MonoBehaviour
{
    [Header("Objective")]
    public string objectDisplayName;

    public Color requiredColor = Color.red;

    private Renderer objectRenderer;

    private bool hasBeenCorrect = false;

    void Awake()
    {
        objectRenderer =
            GetComponentInChildren<Renderer>();
    }

    public bool IsCorrect()
    {
        if (objectRenderer == null)
            return false;

        Color currentColor =
            objectRenderer.material.color;

        return Approximately(
            currentColor,
            requiredColor
        );
    }

    public Color GetRequiredColor()
    {
        return requiredColor;
    }

    public string GetDisplayName()
    {
        if (!string.IsNullOrEmpty(objectDisplayName))
            return objectDisplayName;

        return gameObject.name;
    }

    bool Approximately(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) < 0.05f &&
               Mathf.Abs(a.g - b.g) < 0.05f &&
               Mathf.Abs(a.b - b.b) < 0.05f;
    }
}