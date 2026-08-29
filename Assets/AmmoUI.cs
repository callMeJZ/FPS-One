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

    [Header("Ammo")]
    public int maxAmmo = 6;
    public int currentAmmo = 6;

    void Start()
    {
        currentAmmo = maxAmmo;

        UpdateAmmoUI();
        UpdateColorUI();
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
            colorText.text = GetColorName(selectedColor);
        }
    }

    public void ConsumeAmmo()
    {
        if (currentAmmo <= 0)
            return;

        currentAmmo--;

        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo.ToString("00");
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