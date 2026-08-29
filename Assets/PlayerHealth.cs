using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth = 100;

    [Header("Health UI")]
    public Image[] healthSegments;

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthUI();
    }

    void Update()
{
    //console testing of taking damage
    // if (Input.GetKeyDown(KeyCode.H))
    // {
    //     TakeDamage(20);
    // }
}
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(
            currentHealth,
            0,
            maxHealth
        );

        UpdateHealthUI();

        Debug.Log(
            "Player Health: " +
            currentHealth
        );

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthSegments == null ||
            healthSegments.Length == 0)
        {
            return;
        }

        float healthPerSegment =
            (float)maxHealth /
            healthSegments.Length;

        for (int i = 0;
             i < healthSegments.Length;
             i++)
        {
            float requiredHealth =
                (i + 1) * healthPerSegment;

            if (currentHealth >= requiredHealth)
            {
                // Filled
                healthSegments[i].color =
                    Color.green;
            }
            else
            {
                // Empty
                healthSegments[i].color =
                    Color.white;
            }
        }
    }

    void Die()
    {
        Debug.Log("Player defeated.");
    }
}