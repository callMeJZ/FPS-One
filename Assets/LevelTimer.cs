using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startingTime = 60f;

    [Header("UI")]
    public TMP_Text timerText;

    private float currentTime;
    private bool timerRunning = true;

    void Start()
    {
        currentTime = startingTime;

        UpdateTimerUI();
    }

    void Update()
    {
        if (!timerRunning)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;

            UpdateTimerUI();

            TimerEnded();

            return;
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText == null)
            return;

        int seconds =
            Mathf.CeilToInt(currentTime);

        timerText.text =
            seconds.ToString("00");
    }

    void TimerEnded()
    {
        timerRunning = false;

        Debug.Log(
            "TIME UP! Level failed."
        );
    }
}