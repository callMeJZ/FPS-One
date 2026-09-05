using UnityEngine;
using UnityEngine.UI;

public class TrainingDummy : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI Components")]
    public Image healthBarFill;
    public Canvas healthBarCanvas;

    [Header("Game Statistics")]
    public GameStats gameStats;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (gameStats == null)
        {
            gameStats = FindFirstObjectByType<GameStats>();
        }

        UpdateHealthBar();
    }

    void Update()
    {
        // Keep the health bar facing the camera
        if (healthBarCanvas != null && Camera.main != null)
        {
            healthBarCanvas.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead)
            return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log(
            gameObject.name +
            " took " +
            damageAmount +
            " damage. Remaining health: " +
            currentHealth
        );

        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null && maxHealth > 0f)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (gameStats != null)
        {
            gameStats.RegisterDummyKilled();
        }

        Debug.Log(gameObject.name + " was killed.");

        Destroy(gameObject);
    }
}