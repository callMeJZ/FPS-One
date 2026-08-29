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

    void Start()
    {
        // The statistics window should start hidden.
        gameObject.SetActive(false);
    }

    public void ShowStatistics()
    {
        UpdateStatistics();

        gameObject.SetActive(true);
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

        string stats =
            "Correct Objects: " +
            correct +
            " / " +
            (objectives != null
                ? objectives.objectives.Length
                : 0)
            + "\n\n" +

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