using UnityEngine;

public class Level1GameManager : MonoBehaviour
{
    [Header("Level References")]
    public LevelObjectives objectives;
    public LevelTimer timer;

    private bool levelWon = false;

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

        // Stop the level timer
        if (timer != null)
        {
            timer.StopTimer();
        }

        Debug.Log("LEVEL 1 WON!");
    }

    public bool HasWon()
    {
        return levelWon;
    }
}