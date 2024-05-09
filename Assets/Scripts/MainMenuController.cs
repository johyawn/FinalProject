using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay"); // loading screen
    }

    public void OpenSettings()
    {
        // Implement code to open settings menu (optional)
    }

    public void ViewCredits()
    {
        // Implement code to view credits (optional)
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
    }
}
