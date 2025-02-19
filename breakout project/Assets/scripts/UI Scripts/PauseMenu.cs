using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuUI; // Reference to the pause menu UI panel
    public GameObject PauseText;// Reference to the TextMeshProUGUI element for displaying controls

    private PaddleController pausePower_Ups;
    private BallScript ballScript;
    [HideInInspector] public BackgroundMusic pauseMusic;


    [HideInInspector] public bool isPaused = true; // Flag to track if the game is paused
    private void Start()
    {
        pausePower_Ups = GameObject.Find("Player_Paddle").GetComponent<PaddleController>();
        ballScript = GameObject.Find("Ball").GetComponent<BallScript>();
        pauseMusic =  GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();

    }
    public void TogglePause()
    {
        if (isPaused)
        {
            if (ballScript.lives < 3)
            {


                pauseMusic.LowHealthMusic();
            }
            else if (ballScript.lives > 2)
            {


                pauseMusic.NormalMusic();
            }


            pausePower_Ups.HandleTimers();

            MenuUI.SetActive(false); // Hide the pause menu UI panel

            PauseText.gameObject.SetActive(true); // Hide the controls TextMeshProUGUI element
            Time.timeScale = 1f; // Set the time scale to normal to resume the game
            isPaused = false; // Update the pause state
            return;
        }
            pauseMusic.StopMusic();
            MenuUI.SetActive(true); // Show the pause menu UI panel
            PauseText.gameObject.SetActive(false); // Show the controls TextMeshProUGUI element
            Time.timeScale = 0f; // Set the time scale to 0 to pause the game
            isPaused = true; // Update the pause state






    }

    void Update()
    {


    }
}
