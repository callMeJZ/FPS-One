using UnityEngine;
using UnityEngine.UI;

public class TargetHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float damagePerShot = 10f;

    [Header("Health Bar")]
    public Slider healthBar;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        Debug.Log(
            "Human Target HP: " +
            currentHealth
        );

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log("Human Target defeated.");

        // For now, simply disable the target.
        gameObject.SetActive(false);
    }
}