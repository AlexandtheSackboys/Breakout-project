using UnityEngine;

public class TitleBlock : MonoBehaviour
{

    [SerializeField] private GameObject screenSpawned;
    [SerializeField] private Transform ballSpawner;
    [SerializeField] private GameObject titleBall;
    private PaddleController paddleController;


    private PauseMenu pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        paddleController = GameObject.Find("Player_Paddle").GetComponent<PaddleController>();  
    }

    private void spawnUI()
    {
        if (pauseMenu.isPaused)
        {



            screenSpawned.SetActive(false); // Hide the pause menu UI panel

            pauseMenu.PauseText.gameObject.SetActive(true); // Hide the controls TextMeshProUGUI element
            Time.timeScale = 1f; // Set the time scale to normal to resume the game
            pauseMenu.isPaused = false; // Update the pause state
            return;
        }
        pauseMenu.PauseMusic.StopMusic();
        screenSpawned.SetActive(true); // Show the pause menu UI panel
        pauseMenu.PauseText.gameObject.SetActive(false); // Show the controls TextMeshProUGUI element
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game
        pauseMenu.isPaused = true; // Update the pause state
        return;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Contros are spawned");
            spawnUI();
            titleBall.transform.position = ballSpawner.transform.position;
            paddleController.AimTime();
        }
    }
    

}
