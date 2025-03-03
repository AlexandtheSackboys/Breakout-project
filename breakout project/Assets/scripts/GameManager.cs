using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [HideInInspector] public static bool s_IsMusicPlaying = true;

    [SerializeField] private BackgroundMusic _backgroundMusic;
    [SerializeField] private ScoreSystem _scoreSystem;

    [SerializeField] private IntSO _paddleLives;
    [SerializeField] private int _maxLives;
    [HideInInspector] public static int s_SceneIndex = 0;
    public static GameManager s_Instance;

    private void Awake()
    {
        s_Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DontDestroyOnLoad(s_Instance);
    }

    public void Play() //  causes main game to play
    {
        _backgroundMusic.StopMusic();
        ScoreSystem.s_ScoreCount = 0;
       _paddleLives.CharacterLives = _maxLives;

        s_SceneIndex = 1;

        s_IsMusicPlaying = true;
        SceneManager.LoadScene(s_SceneIndex);


    }


    public void Quit() // exits game 
    {
        Application.Quit(0);
    }
    public void End() // transisions to winning scene
    {

        _backgroundMusic.StopMusic();
        s_SceneIndex = 3;
        s_IsMusicPlaying = false;
        SceneManager.LoadScene(s_SceneIndex);




    }

    public void NextLevel() // transisions to winning scene
    {

        _paddleLives.CharacterLives += 2;
        _backgroundMusic.StopMusic();
        s_SceneIndex = 2;
        s_IsMusicPlaying = true;
        SceneManager.LoadScene(s_SceneIndex);




    }


    public void ResetGame()
    { // reset to the games title screen
        ScoreSystem.s_ScoreCount = 0;


        _paddleLives.CharacterLives = _maxLives;
        s_SceneIndex = 0;
        SceneManager.LoadScene(s_SceneIndex);
        s_IsMusicPlaying = true;
        Debug.Log("reset presses i guess");
    }

}

