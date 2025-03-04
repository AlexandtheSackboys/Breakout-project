using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]private bool continueGame = true;

    private BackgroundMusic endTitle;

    private void Start()
    {

        endTitle = GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!continueGame)
            {
                GameManager.Instance.Quit();
                return;
            }
            endTitle.StopMusic();
            GameManager.Instance.Play();

        }
    }
}
