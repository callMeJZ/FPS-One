using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1GameManager : MonoBehaviour
{
    [Header("Level Settings")]
    public int levelNumber = 1;

    [Header("Level References")]
    public LevelObjectives objectives;
    public LevelTimer timer;
    public AmmoUI ammoUI;

    [Header("Statistics")]
    public Level1StatisticsUI statisticsUI;

    [Header("Lose UI")]
    public Level1LoseUI loseUI;
    [Header("Final Game UI")]
    public GameCompleteUI gameCompleteUI;
    private bool levelWon = false;
    private bool levelLost = false;

    void Start()
    {
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

        if (ammoUI == null)
        {
            ammoUI =
                FindFirstObjectByType<AmmoUI>();
        }

        if (statisticsUI == null)
        {
            statisticsUI =
                FindFirstObjectByType<Level1StatisticsUI>();
        }

        if (loseUI == null)
        {
            loseUI =
                FindFirstObjectByType<Level1LoseUI>();
        }

        Debug.Log(
            "Level " +
            levelNumber +
            " Game Manager started."
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
        // Timer failure
        if (timer != null &&
            timer.HasTimeExpired())
        {
            LoseLevel("TIME RAN OUT!");
            return;
        }

        // Ammo failure
        if (ammoUI != null &&
            ammoUI.GetTotalRemainingAmmo() <= 0)
        {
            if (objectives == null ||
                !objectives.AreObjectivesComplete())
            {
                LoseLevel("OUT OF AMMO!");
            }
        }
    }

    void WinLevel()
    {
        levelWon = true;

        if (timer != null)
        {
            timer.StopTimer();
        }

        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible = true;

        Debug.Log("==============================");

        Debug.Log(
            "LEVEL " +
            levelNumber +
            " WON!"
        );

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

    void LoseLevel(string reason)
    {
        levelLost = true;

        if (timer != null)
        {
            timer.StopTimer();
        }

        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible = true;

        Debug.Log("==============================");

        Debug.Log(
            "LEVEL " +
            levelNumber +
            " LOST!"
        );

        Debug.Log(reason);

        Debug.Log("==============================");

        if (loseUI != null)
        {
            loseUI.ShowLoseWindow(reason);
        }
        else
        {
            Debug.LogError(
                "Lose UI is NOT assigned!"
            );
        }
    }

    public void RetryLevel()
    {
        Scene currentScene =
            SceneManager.GetActiveScene();

        SceneManager.LoadScene(
            currentScene.buildIndex
        );
    }

    public void ContinueToLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void FinishGame()
{
    Debug.Log("==============================");
    Debug.Log("GAME COMPLETED!");
    Debug.Log("==============================");

    Cursor.lockState =
        CursorLockMode.None;

    Cursor.visible = true;

    // Stop time just in case.
    if (timer != null)
    {
        timer.StopTimer();
    }
    if (gameCompleteUI != null)
    {
        gameCompleteUI.ShowGameComplete();
    }
    else
    {
        Debug.LogError(
            "Game Complete UI is NOT assigned!"
        );
    }
    // Temporarily stop gameplay.
    //Time.timeScale = 0f;
}
public void RestartEntireGame()
{
    Time.timeScale = 1f;

    SceneManager.LoadScene("Level1");
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