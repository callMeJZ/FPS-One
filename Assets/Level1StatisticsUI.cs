using UnityEngine;
using TMPro;

public class Level1StatisticsUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text titleText;
    public TMP_Text statisticsText;

    [Header("Level References")]
    public LevelObjectives objectives;
    public LevelTimer timer;
    public AmmoUI ammoUI;

    public void ShowStatistics()
    {
        // Update the displayed statistics first
        UpdateStatistics();

        // Then show the window
        gameObject.SetActive(true);

        Debug.Log("Statistics Window shown.");
    }

    void UpdateStatistics()
    {
        if (titleText != null)
        {
            titleText.text =
                "LEVEL 1 COMPLETE!";
        }

        int correct = 0;
        int incorrect = 0;

        if (objectives != null)
        {
            correct =
                objectives.correctObjects;

            incorrect =
                objectives.incorrectObjects;
        }

        float remainingTime = 0f;

        if (timer != null)
        {
            remainingTime =
                timer.GetRemainingTime();
        }

        int remainingAmmo = 0;

        if (ammoUI != null)
        {
            remainingAmmo =
                ammoUI.GetTotalRemainingAmmo();
        }

        int totalObjectives = 0;

        if (objectives != null)
        {
            totalObjectives =
                objectives.objectives.Length;
        }

        string stats =
            "Correct Objects: " +
            correct +
            " / " +
            totalObjectives +
            "\n\n" +

            "Incorrect Attempts: " +
            incorrect +
            "\n\n" +

            "Remaining Time: " +
            Mathf.CeilToInt(remainingTime) +
            " s\n\n" +

            "Remaining Ammo: " +
            remainingAmmo;

        if (statisticsText != null)
        {
            statisticsText.text = stats;
        }
    }
}