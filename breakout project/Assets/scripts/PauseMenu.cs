using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI panel
    public GameObject PauseText;// Reference to the TextMeshProUGUI element for displaying controls

    private bool isPaused = false; // Flag to track if the game is paused

    public void TogglePause()
    {
        if (isPaused)
        {
            pauseMenuUI.SetActive(false); // Hide the pause menu UI panel
            PauseText.gameObject.SetActive(true); // Hide the controls TextMeshProUGUI element
            Time.timeScale = 1f; // Set the time scale to normal to resume the game
            isPaused = false; // Update the pause state flag
            return;
        }
        pauseMenuUI.SetActive(true); // Show the pause menu UI panel
        PauseText.gameObject.SetActive(false); // Show the controls TextMeshProUGUI element
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game
        isPaused = true; // Update the pause state flag
    }

    void Update()
    {


    }
}
