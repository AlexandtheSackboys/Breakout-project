using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]private bool _continueGame = true;

    private BackgroundMusic _endTitle;

    private void Start()
    {

        _endTitle = GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_continueGame)
            {
                GameManager.Instance.Quit();
                return;
            }
            _endTitle.StopMusic();
            GameManager.Instance.Play();

        }
    }
}
