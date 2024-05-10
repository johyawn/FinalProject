using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Death_Trigger : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    private float followSpeed = 0.8f; // Speed at which the game object follows the player
    public Button positionButton;
    // Start is called before the first frame update
    void Start()
    {
        positionButton.onClick.AddListener(OnPositionButtonClick);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the target position for the game object to follow the player
        Vector3 targetPosition = player.transform.position;

        // Move the game object towards the target position at the specified follow speed
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Calculate the direction from the game object to the player
        Vector3 directionToPlayer = player.transform.position - transform.position;

        // Calculate the rotation needed to face the player, including a rotation offset
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer) * Quaternion.Euler(0, 180, 0);

        // Apply the rotation to the game object
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, followSpeed * 5 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            GameOver();
        }
    }

    private void GameOver()
    {
        // Add your game over logic here
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
        Time.timeScale = 1;

    }
    private void OnPositionButtonClick()
    {
        // Add your logic for the position button click here
        Debug.Log("Position button clicked");
    }
}
