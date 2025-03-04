using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [HideInInspector] public static bool IsMusicPlaying = true;

    [SerializeField] private BackgroundMusic _backgroundMusic;
    [SerializeField] private ScoreSystem _scoreSystem;

    [SerializeField] private IntSO _paddleLives;
    [SerializeField] private int _maxLives;
    [HideInInspector] public static int SceneIndex = 0;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DontDestroyOnLoad(Instance);
    }

    public void Play() //  causes main game to play and sends player to first board
    {
        _backgroundMusic.StopMusic();
        ScoreSystem.ScoreCount = 0;
       _paddleLives.CharacterLives = _maxLives;

        SceneIndex = 1;

        IsMusicPlaying = true;
        SceneManager.LoadScene(SceneIndex);


    }


    public void Quit() // exits game 
    {
        Application.Quit(0);
    }
    public void End() // transisions to winning scene
    {

        _backgroundMusic.StopMusic();
        SceneIndex = 3;
        IsMusicPlaying = false;
        SceneManager.LoadScene(SceneIndex);




    }

    public void NextLevel() // transisions to winning scene
    {

        _paddleLives.CharacterLives += 2;
        _backgroundMusic.StopMusic();
        SceneIndex = 2;
        IsMusicPlaying = true;
        SceneManager.LoadScene(SceneIndex);




    }


    public void ResetGame()
    { // reset to the games title screen
        ScoreSystem.ScoreCount = 0;


        _paddleLives.CharacterLives = _maxLives;
        SceneIndex = 0;
        SceneManager.LoadScene(SceneIndex);
        IsMusicPlaying = true;
        Debug.Log("reset presses i guess");
    }

}

