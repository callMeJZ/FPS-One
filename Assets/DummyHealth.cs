using UnityEngine;
using UnityEngine.UI;

public class DummyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI Settings")]
    public Image healthBarFill;
    public Canvas healthCanvas; // Canvas na laging haharap sa camera

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Para laging nakaharap ang health bar sa paningin ng player
        if (healthCanvas != null && Camera.main != null)
        {
            healthCanvas.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Nabawasan ang Dummy! Natitirang Buhay: " + currentHealth);

        // Ina-update ang porsyento ng green bar
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Namatay ang Dummy!");
        Destroy(gameObject);
    }
}