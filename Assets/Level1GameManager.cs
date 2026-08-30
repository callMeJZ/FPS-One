using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1GameManager : MonoBehaviour
{
    [Header("Level References")]
    public LevelObjectives objectives;
    public LevelTimer timer;

    [Header("Statistics")]
    public Level1StatisticsUI statisticsUI;

    private bool levelWon = false;
    private bool levelLost = false;

    void Start()
    {
        // Automatically find references if they are not assigned.
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
        if (levelWon || levelLost)
            return;

        CheckWinCondition();

        if (levelWon)
            return;

        CheckLoseCondition();
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

    void CheckLoseCondition()
    {
        if (timer == null)
            return;

        if (timer.HasTimeExpired())
        {
            LoseLevel();
        }
    }

    void WinLevel()
    {
        levelWon = true;

        if (timer != null)
        {
            timer.StopTimer();
        }

        Debug.Log("==============================");
        Debug.Log("LEVEL 1 WON!");
        Debug.Log("==============================");

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

    void LoseLevel()
    {
        levelLost = true;

        Debug.Log("==============================");
        Debug.Log("LEVEL 1 LOST!");
        Debug.Log("TIME RAN OUT!");
        Debug.Log("==============================");

        // Restart Level 1.
        RestartLevel();
    }

    void RestartLevel()
    {
        Scene currentScene =
            SceneManager.GetActiveScene();

        SceneManager.LoadScene(
            currentScene.buildIndex
        );
    }

    public bool HasWon()
    {
        return levelWon;
    }

    public bool HasLost()
    {
        return levelLost;
    }
}