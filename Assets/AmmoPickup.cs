using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private AmmoUI ammoUI;

    private void Start()
    {
        // Automatically find the AmmoUI in the current scene.
        ammoUI = FindFirstObjectByType<AmmoUI>();

        if (ammoUI == null)
        {
            Debug.LogError("AmmoUI could not be found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (ammoUI != null)
        {
            ammoUI.RefillAmmo();
            Debug.Log("Ammo pickup collected. Ammo refilled.");
        }

        Destroy(gameObject);
    }
}