using UnityEngine;
using UnityEngine.UI;

public class BulletColorPicker : MonoBehaviour
{
    [Header("Selected Color Display")]
    public Image selectedColorIndicator;

    [Header("Color Indicators")]
    public Image redButton;
    public Image blueButton;
    public Image greenButton;
    public Image yellowButton;
    public Image purpleButton;
    public Image orangeButton;

    private void Start()
    {
        // Keep the current selected-color display visible
        // without controlling bullet color.
        if (selectedColorIndicator != null)
        {
            selectedColorIndicator.color = Color.white;
        }
    }
}