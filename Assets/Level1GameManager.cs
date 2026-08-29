using UnityEngine;

public class Level1GameManager : MonoBehaviour
{
    [Header("Level References")]
    public LevelObjectives objectives;
    public LevelTimer timer;

    [Header("Statistics")]
    public Level1StatisticsUI statisticsUI;

    private bool levelWon = false;

    void Start()
    {
        // Automatically find references if they were not assigned.
        if (objectives == null)
        {
            objectives =
                FindFirstObjectByType<LevelObjectives>();
        }

        if (timer == null)
        {
            timer =
                FindFirstObjectByType<LevelTimer>();
        }

        Debug.Log(
            "Level1GameManager started."
        );

        Debug.Log(
            "Objectives reference: " +
            (objectives != null
                ? objectives.name
                : "NULL")
        );

        Debug.Log(
            "Timer reference: " +
            (timer != null
                ? timer.name
                : "NULL")
        );

        Debug.Log(
            "Statistics UI reference: " +
            (statisticsUI != null
                ? statisticsUI.name
                : "NULL")
        );
    }

    void Update()
    {
        if (levelWon)
            return;

        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (objectives == null)
            return;

        if (objectives.AreObjectivesComplete())
        {
            WinLevel();
        }
    }

    void WinLevel()
    {
        levelWon = true;

        // Stop the timer.
        if (timer != null)
        {
            timer.StopTimer();
        }

        Debug.Log("==============================");
        Debug.Log("LEVEL 1 WON!");
        Debug.Log("==============================");

        // Show statistics window.
        if (statisticsUI != null)
        {
            statisticsUI.ShowStatistics();
        }
        else
        {
            Debug.LogError(
                "Statistics UI is NOT assigned!"
            );
        }
    }

    public bool HasWon()
    {
        return levelWon;
    }
}