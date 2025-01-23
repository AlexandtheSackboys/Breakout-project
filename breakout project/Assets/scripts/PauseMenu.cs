using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI panel
    public TextMeshProUGUI controlsText; // Reference to the TextMeshProUGUI element for displaying controls

    private bool isPaused = false; // Flag to track if the game is paused

    void Update()
    {
        // Check if the player presses the escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Exit the application
            Application.Quit();
        }
        // Check if the player presses the Escape key to toggle pause state and show/hide controls
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume(); // If paused, resume the game
            }
            else
            {
                Pause(); // If not paused, pause the game and show controls

            }
        }
    }

    // Resume the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu UI panel
        controlsText.gameObject.SetActive(false); // Hide the controls TextMeshProUGUI element
        Time.timeScale = 1f; // Set the time scale to normal to resume the game
        isPaused = false; // Update the pause state flag
    }

    // Pause the game and show controls
    void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu UI panel
        controlsText.gameObject.SetActive(true); // Show the controls TextMeshProUGUI element
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game
        isPaused = true; // Update the pause state flag
    }
}
