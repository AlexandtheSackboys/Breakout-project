using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]private bool continueGame = true;
    private GameManager gameManager;
    private backgroundMusic endTitle;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        endTitle = GameObject.Find("BackgroundMusic_emitter").GetComponent<backgroundMusic>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!continueGame)
            {
                gameManager.Quit();
                return;
            }
            endTitle.StopMusic();
            gameManager.Play();

        }
    }
}
