using UnityEngine;
using UnityEngine.UI;

public class TrainingDummy : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI Components")]
    // Gumamit ng UI Image component na naka-set ang Image Type sa "Filled"
    public Image healthBarFill; 
    public Canvas healthBarCanvas;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Para laging nakaharap sa player/camera ang Health Bar (Billboard effect)
        if (healthBarCanvas != null && Camera.main != null)
        {
            healthBarCanvas.transform.rotation = Camera.main.transform.rotation;
        }
    }

    // Tatawagin ang function na ito ng iyong Player Shooting/Gun script kapag tinamaan ito
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Hindi bababa sa 0 ang health

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            // Ina-update ang porsyento ng green bar base sa natitirang health
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        // Pwedeng maglagay ng particle effects dito bago masira
        Destroy(gameObject);
    }
}