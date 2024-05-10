using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
