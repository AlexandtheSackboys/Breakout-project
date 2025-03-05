using UnityEngine;

public class TitleBlock : MonoBehaviour
{

    [SerializeField] private GameObject _screenSpawned;
    [SerializeField] private Transform _ballSpawner;
    [SerializeField] private GameObject _titleBall;
    private PaddleController _paddleController;


    private PauseMenu _pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        _paddleController = GameObject.Find("Player_Paddle").GetComponent<PaddleController>();

    }

    private void spawnUI()
    {
        if (_pauseMenu.isPaused)
        {



            _screenSpawned.SetActive(false); // Hide the pause menu UI panel

            _pauseMenu.PauseText.gameObject.SetActive(true); // Hide the controls TextMeshProUGUI element
            Time.timeScale = 1f; // Set the time scale to normal to resume the game
            _pauseMenu.isPaused = false; // Update the pause state
            return;
        }
        _pauseMenu.PauseMusic.StopMusic();
        _screenSpawned.SetActive(true); // Show the pause menu UI panel
        _pauseMenu.PauseText.gameObject.SetActive(false); // Show the controls TextMeshProUGUI element
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game
        _pauseMenu.isPaused = true; // Update the pause state
        return;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Contros are spawned");
            spawnUI();
            _titleBall.transform.position = _ballSpawner.transform.position;
            _paddleController.AimTime();
            _pauseMenu.MenuUI = _screenSpawned;
        }
    }
    

}
