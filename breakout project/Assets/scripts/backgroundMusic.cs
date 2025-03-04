using FMODUnity;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{


    [SerializeField] private BallScript _ballHealth;
    [SerializeField] private StudioEventEmitter _musicEmitter;
    [SerializeField] private IntSO _sceneLives;


    ParamRef[] _musicParamaters;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _musicParamaters = _musicEmitter.Params;
        if (GameManager.IsMusicPlaying == true)
        {
           
            adaptiveMusic();
            return;
        }
        _musicEmitter.Stop();


    }


    public void LowHealthMusic() // function will play when health is less than 3
    {
        _musicEmitter.Stop();
        _musicParamaters[0].Value = 1;
        _musicEmitter.Play();
        return;
    }

    public void NormalMusic() // function will play when health is greater than 2
    {
        _musicEmitter.Stop();
        _musicParamaters[0].Value = 0;
        _musicEmitter.Play();
        return;
    }
    public void StopMusic() // will make music stop across (which will happen when it is called in other scripts)
    {
        _musicEmitter.Stop();
    }




    void adaptiveMusic()
    {
        if (_sceneLives.CharacterLives > 2)
        {
            Debug.Log("music playing");
            NormalMusic();
            return;
        }
        else
        {
            LowHealthMusic();
        }



    }
}
