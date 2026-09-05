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
    public int maxAmmo = 30;

    private int currentAmmo;

    void Start()
    {
        currentAmmo = maxAmmo;

        UpdateAmmoUI();

        // Keep the old UI objects from causing errors,
        // but they are no longer used for shooting.
        if (currentBulletColor != null)
        {
            currentBulletColor.gameObject.SetActive(false);
        }

        if (colorText != null)
        {
            colorText.gameObject.SetActive(false);
        }
    }

    public void ConsumeAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
        }

        UpdateAmmoUI();
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetTotalRemainingAmmo()
    {
        return currentAmmo;
    }

    public void RefillAmmo()
    {
        currentAmmo = maxAmmo;

        UpdateAmmoUI();

        Debug.Log("Ammo refilled!");
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo.ToString("00");
        }
    }
}