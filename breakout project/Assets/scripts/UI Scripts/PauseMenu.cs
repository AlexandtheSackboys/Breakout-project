using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector]public GameObject MenuUI; // Reference to the pause menu UI panel
    public GameObject PauseText;// Reference to the TextMeshProUGUI element for displaying controls

    private PaddleController _pauseGameplay;
    private BallScript _ballScript;
    [HideInInspector] public BackgroundMusic PauseMusic;
    [SerializeField] private GameObject _controlsUI, _powersUI,_mainUI;


    [HideInInspector] public bool isPaused = false; // Flag to track if the game is paused
    void Start()
    {
        _pauseGameplay = GameObject.Find("Player_Paddle").GetComponent<PaddleController>();
        _ballScript = GameObject.Find("Ball").GetComponent<BallScript>();
        PauseMusic =  GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();
        MenuUI = _mainUI;


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


            _pauseGameplay.HandleTimers();

            MenuUI.SetActive(false); // Hide the pause menu UI panel
            MenuUI = _mainUI;
            PauseText.gameObject.SetActive(true); // shows the controls TextMeshProUGUI element
            Time.timeScale = 1f; // Set the time scale to normal to resume the game
            isPaused = false; // Update the pause state
            return;
        }
            PauseMusic.StopMusic();

        MenuUI.SetActive(true); // Show the pause menu UI panel

        PauseText.gameObject.SetActive(false); // hides the controls TextMeshProUGUI element
            Time.timeScale = 0f; // Set the time scale to 0 to pause the game
            isPaused = true; // Update the pause state
    }

    public void CurrentUI(int indexUI)
    {
        // in editor enter 0 to acess the controls explanations
        if (indexUI == 0)
        {
            MenuUI = _controlsUI;
            return;
        }
        // in editor enter 1 to access the controls
        if (indexUI == 1)
        {
            MenuUI = _powersUI;
        }
        // in editor enter any onther number to access the main menu
        else
        {
            MenuUI = _mainUI;
        }

    }

        





}
