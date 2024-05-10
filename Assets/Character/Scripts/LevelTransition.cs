using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding GameObject has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Load the next scene if nextSceneName is not empty
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("Next scene name is not specified!");
            }
        }
    }
}
