using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [Header("Gun Reference")]
    public GunMechanics gun;

    [Header("UI References")]
    public Image currentBulletColor;
    public TMP_Text colorText;
    public TMP_Text ammoText;

    [Header("Ammo Per Color")]
    public int maxAmmoPerColor = 6;

    private int redAmmo;
    private int blueAmmo;
    private int greenAmmo;
    private int yellowAmmo;
    private int purpleAmmo;
    private int orangeAmmo;

    private Color lastColor;

    void Start()
    {
        // Give every color its own ammo pool.
        redAmmo = maxAmmoPerColor;
        blueAmmo = maxAmmoPerColor;
        greenAmmo = maxAmmoPerColor;
        yellowAmmo = maxAmmoPerColor;
        purpleAmmo = maxAmmoPerColor;
        orangeAmmo = maxAmmoPerColor;

        if (gun != null)
        {
            lastColor = gun.currentBulletColor;
        }

        UpdateAmmoUI();
        UpdateColorUI();
    }

    void Update()
    {
        if (gun == null)
            return;

        // Detect a color change.
        if (gun.currentBulletColor != lastColor)
        {
            lastColor = gun.currentBulletColor;

            UpdateColorUI();
            UpdateAmmoUI();
        }
    }

    public void UpdateColorUI()
    {
        if (gun == null)
            return;

        Color selectedColor = gun.currentBulletColor;

        if (currentBulletColor != null)
        {
            currentBulletColor.color = selectedColor;
        }

        if (colorText != null)
        {
            colorText.text =
                GetColorName(selectedColor);
        }
    }

    public void ConsumeAmmo()
    {
        Color color = gun.currentBulletColor;

        if (Approximately(color, Color.red))
        {
            if (redAmmo > 0)
                redAmmo--;
        }
        else if (Approximately(color, Color.blue))
        {
            if (blueAmmo > 0)
                blueAmmo--;
        }
        else if (Approximately(color, Color.green))
        {
            if (greenAmmo > 0)
                greenAmmo--;
        }
        else if (Approximately(color, Color.yellow))
        {
            if (yellowAmmo > 0)
                yellowAmmo--;
        }
        else if (Approximately(
            color,
            new Color(0.6f, 0.2f, 1f)))
        {
            if (purpleAmmo > 0)
                purpleAmmo--;
        }
        else if (Approximately(
            color,
            new Color(1f, 0.5f, 0f)))
        {
            if (orangeAmmo > 0)
                orangeAmmo--;
        }

        UpdateAmmoUI();
    }

    public int GetCurrentAmmo()
    {
        Color color = gun.currentBulletColor;

        if (Approximately(color, Color.red))
            return redAmmo;

        if (Approximately(color, Color.blue))
            return blueAmmo;

        if (Approximately(color, Color.green))
            return greenAmmo;

        if (Approximately(color, Color.yellow))
            return yellowAmmo;

        if (Approximately(
            color,
            new Color(0.6f, 0.2f, 1f)))
        {
            return purpleAmmo;
        }

        if (Approximately(
            color,
            new Color(1f, 0.5f, 0f)))
        {
            return orangeAmmo;
        }

        return 0;
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text =
                GetCurrentAmmo().ToString("00");
        }
    }

    string GetColorName(Color color)
    {
        if (Approximately(color, Color.red))
            return "RED";

        if (Approximately(color, Color.blue))
            return "BLUE";

        if (Approximately(color, Color.green))
            return "GREEN";

        if (Approximately(color, Color.yellow))
            return "YELLOW";

        if (Approximately(
            color,
            new Color(0.6f, 0.2f, 1f)))
        {
            return "PURPLE";
        }

        if (Approximately(
            color,
            new Color(1f, 0.5f, 0f)))
        {
            return "ORANGE";
        }

        return "CUSTOM";
    }

    bool Approximately(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) < 0.05f &&
               Mathf.Abs(a.g - b.g) < 0.05f &&
               Mathf.Abs(a.b - b.b) < 0.05f;
    }
}