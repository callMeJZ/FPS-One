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
        // Automatically find references if not assigned.
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

        if (statisticsUI == null)
        {
            statisticsUI =
                FindFirstObjectByType<Level1StatisticsUI>();
        }
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

        // Stop timer
        if (timer != null)
        {
            timer.StopTimer();
        }

        Debug.Log("==============================");
        Debug.Log("LEVEL 1 WON!");
        Debug.Log("==============================");

        // Show statistics
        if (statisticsUI != null)
        {
            statisticsUI.ShowStatistics();
        }
    }

    public bool HasWon()
    {
        return levelWon;
    }
}