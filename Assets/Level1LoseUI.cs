using UnityEngine;
using TMPro;

public class Level1LoseUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text loseReasonText;

    public void ShowLoseWindow(string reason)
    {
        if (loseReasonText != null)
        {
            loseReasonText.text = reason;
        }

        gameObject.SetActive(true);

        Debug.Log(
            "Lose Window shown: " + reason
        );
    }

    public void HideLoseWindow()
    {
        gameObject.SetActive(false);
    }
}