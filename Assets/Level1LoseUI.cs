using UnityEngine;

public class Level1LoseUI : MonoBehaviour
{
    public void ShowLoseWindow()
    {
        gameObject.SetActive(true);

        Debug.Log("Lose Window shown.");
    }

    public void HideLoseWindow()
    {
        gameObject.SetActive(false);
    }
}