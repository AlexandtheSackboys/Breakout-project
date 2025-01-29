using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI panel
    public GameObject PauseText;// Reference to the TextMeshProUGUI element for displaying controls

    private bool isPaused = false; // Flag to track if the game is paused

    void TogglePause()
    {
        if (isPaused)
        {
            Resume();
            return;
        }
        Pause();
    }

    void CheckInputs()
    {
        // Check if the player presses the Escape key to toggle pause state and show/hide controls
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void Resume() 
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu UI panel
        PauseText.gameObject.SetActive(true); // Hide the controls TextMeshProUGUI element
        Time.timeScale = 1f; // Set the time scale to normal to resume the game
        isPaused = false; // Update the pause state flag
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu UI panel
        PauseText.gameObject.SetActive(false); // Show the controls TextMeshProUGUI element
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game
        isPaused = true; // Update the pause state flag
    }
    void Update()
    {
        CheckInputs();

    }
}
