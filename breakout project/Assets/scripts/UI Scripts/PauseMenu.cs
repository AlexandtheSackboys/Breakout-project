using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuUI; // Reference to the pause menu UI panel
    public GameObject PauseText;// Reference to the TextMeshProUGUI element for displaying controls

    private PaddleController _pausePowerUps;
    private BallScript _ballScript;
    [HideInInspector] public BackgroundMusic PauseMusic;


    [HideInInspector] public bool isPaused = true; // Flag to track if the game is paused
    void Start()
    {
        _pausePowerUps = GameObject.Find("Player_Paddle").GetComponent<PaddleController>();
        _ballScript = GameObject.Find("Ball").GetComponent<BallScript>();
        PauseMusic =  GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();

    }
    public void TogglePause()
    {
        if (isPaused)
        {
            if (_ballScript.Lives.CharacterLives < 3)
            {


                PauseMusic.LowHealthMusic();
            }
            else if (_ballScript.Lives.CharacterLives > 2)
            {


                PauseMusic.NormalMusic();
            }


            _pausePowerUps.HandleTimers();

            MenuUI.SetActive(false); // Hide the pause menu UI panel

            PauseText.gameObject.SetActive(true); // Hide the controls TextMeshProUGUI element
            Time.timeScale = 1f; // Set the time scale to normal to resume the game
            isPaused = false; // Update the pause state
            return;
        }
            PauseMusic.StopMusic();
            MenuUI.SetActive(true); // Show the pause menu UI panel
            PauseText.gameObject.SetActive(false); // Show the controls TextMeshProUGUI element
            Time.timeScale = 0f; // Set the time scale to 0 to pause the game
            isPaused = true; // Update the pause state
    }

    void Update()
    {


    }
}
