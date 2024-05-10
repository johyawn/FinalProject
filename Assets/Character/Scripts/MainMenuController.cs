using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play button clicked! Starting the game...");
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
        // Implement code to open settings menu (optional)
        Debug.Log("Open settings menu"); // Placeholder debug log
    }

    public void ViewCredits()
    {
        // Implement code to view credits (optional)
        Debug.Log("View credits"); // Placeholder debug log
    }

    public void QuitGame()
    {
        // Quit the application (only works in builds, not in editor)
        Application.Quit();
    }

    
}
