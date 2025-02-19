using UnityEngine;
using FMODUnity;

public class BackgroundMusic : MonoBehaviour
{

    public BallScript ball_Health;
    [SerializeField] private StudioEventEmitter music;
    private StudioEventEmitter eventEmitter;
    ParamRef[] paramaters;
    [SerializeField] private GameManager gameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        paramaters = music.Params;
        if (gameManager.ismusicPlaying == true)
        {
            NormalMusic();
            return;
        }
        music.Stop();


    }


    public void LowHealthMusic()
    {
        music.Stop();
        paramaters[0].Value = 1;
        music.Play();
        return;
    }

    public void NormalMusic()
    {
        music.Stop();
        paramaters[0].Value = 0;
        music.Play();
        return;
    }
    public void StopMusic()
    {
        music.Stop();
    }



    // Update is called once per frame
    void Update()
    {

    }
}
