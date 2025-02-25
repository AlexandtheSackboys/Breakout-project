using FMODUnity;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{


    [SerializeField] private BallScript ballHealth;
    [SerializeField] private StudioEventEmitter music;
    [SerializeField] private IntSO sceneLives;
    private StudioEventEmitter eventEmitter;
    ParamRef[] paramaters;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        paramaters = music.Params;
        if (GameManager.Instance.ismusicPlaying == true)
        {
            if (sceneLives.CharacterLives > 2)
            {
                NormalMusic();
                return;
            }
            LowHealthMusic();
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
