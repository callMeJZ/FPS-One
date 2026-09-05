using UnityEngine;

public class TrainingDummyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log("Training Dummy Health: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Training Dummy Killed!");

        Destroy(gameObject);
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }
}