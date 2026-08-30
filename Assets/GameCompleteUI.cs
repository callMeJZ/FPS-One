using UnityEngine;

public class GameCompleteUI : MonoBehaviour
{
    public void ShowGameComplete()
    {
        gameObject.SetActive(true);

        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible = true;

        Time.timeScale = 0f;

        Debug.Log("GAME COMPLETE WINDOW SHOWN.");
    }

    public void HideGameComplete()
    {
        gameObject.SetActive(false);
    }
}