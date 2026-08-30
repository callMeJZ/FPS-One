using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // Make sure the game is not paused.
        Time.timeScale = 1f;

        // Show the mouse cursor for menu interaction.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Debug.Log("EXIT GAME BUTTON PRESSED.");

        #if UNITY_EDITOR
        // When testing inside the Unity Editor,
        // stop Play Mode instead of closing Unity.
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // When running the built game,
        // close the application.
        Application.Quit();
        #endif
    }
}