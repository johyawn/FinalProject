using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour
{
    public GameObject player; // Reference to the player object

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject); // Destroy the player object
            GameOver();
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void GameOver()
    {
        // Add your game over logic here
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver"); // Transition to the GameOverScene
    }
}
