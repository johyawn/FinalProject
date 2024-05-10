using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector;

    private bool cutSceneOver = false;

    void Start()
    {
        // Subscribe to the Director's stopped event
        playableDirector.stopped += OnCutSceneFinished;
    }

    void OnCutSceneFinished(PlayableDirector director)
    {
        // Check if the stopped Director is the same as the one we're listening to
        if (director == playableDirector && !cutSceneOver)
        {
            // Load the Main Menu scene
            SceneManager.LoadScene("mainmenu");
            cutSceneOver = true;
        }
    }
}
