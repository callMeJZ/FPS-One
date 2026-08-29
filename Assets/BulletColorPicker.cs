using UnityEngine;
using UnityEngine.UI;

public class BulletColorPicker : MonoBehaviour
{
    [Header("Gun Reference")]
    public GunMechanics gun;

    [Header("Ammo UI")]
    public AmmoUI ammoUI;
    [Header("Selected Color Display")]
    public Image selectedColorIndicator;

    [Header("Color Indicators")]
    public Image redButton;
    public Image blueButton;
    public Image greenButton;
    public Image yellowButton;
    public Image purpleButton;
    public Image orangeButton;

    private Color selectedColor = Color.red;

    void Start()
    {
        // Default color
        SetColor(Color.red);

        Debug.Log("Bullet Color Controls:");
        Debug.Log("1 = Red");
        Debug.Log("2 = Blue");
        Debug.Log("3 = Green");
        Debug.Log("4 = Yellow");
        Debug.Log("5 = Purple");
        Debug.Log("6 = Orange");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetColor(Color.red);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetColor(Color.blue);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetColor(Color.green);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetColor(Color.yellow);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetColor(new Color(0.6f, 0.2f, 1f));
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetColor(new Color(1f, 0.5f, 0f));
        }
    }

    void SetColor(Color newColor)
{
    selectedColor = newColor;

    // Update the gun's current bullet color
    if (gun != null)
    {
        gun.currentBulletColor = selectedColor;
    }

    // Update the color picker indicator
    if (selectedColorIndicator != null)
    {
        selectedColorIndicator.color = selectedColor;
    }

    // Update the ammo/current-color UI
    if (ammoUI != null)
    {
        ammoUI.UpdateColorUI();
    }

    Debug.Log(
        "Current Bullet Color: " +
        selectedColor
    );
}
}