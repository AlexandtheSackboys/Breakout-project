using UnityEngine;
using FMODUnity;

public class backgroundMusic : MonoBehaviour
{

    public BallScript ball_Health;
    [SerializeField] private StudioEventEmitter music;
    private StudioEventEmitter eventEmitter;
    ParamRef[] paramaters;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //eventEmitter = new StudioEventEmitter();
        //FMOD.Studio.PARAMETER_ID switchMusic;
        //Debug.Log(switchMusic);
        paramaters = music.Params;
        if (!music.IsPlaying())
        {
            NormalMusic();
        }
        //main.setParameterByName("Region A",1);
        //main.start();
        //main.release();


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
