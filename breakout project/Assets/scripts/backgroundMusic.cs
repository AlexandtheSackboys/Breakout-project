using FMODUnity;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{


    [SerializeField] private BallScript ballHealth;
    [SerializeField] private StudioEventEmitter musicEmitter;
    [SerializeField] private IntSO sceneLives;

    ParamRef[] paramaters;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        paramaters = musicEmitter.Params;
        if (GameManager.IsMusicPlaying == true)
        {
            adaptiveMusic();
            return;
        }
        musicEmitter.Stop();


    }


    public void LowHealthMusic()
    {
        musicEmitter.Stop();
        paramaters[0].Value = 1;
        musicEmitter.Play();
        return;
    }

    public void NormalMusic()
    {
        musicEmitter.Stop();
        paramaters[0].Value = 0;
        musicEmitter.Play();
        return;
    }
    public void StopMusic()
    {
        musicEmitter.Stop();
    }



    // Update is called once per frame
    void Update()
    {

    }

    void adaptiveMusic()
    {
        if (sceneLives.CharacterLives > 2)
        {
            NormalMusic();
            return;
        }
        LowHealthMusic();


    }
}
