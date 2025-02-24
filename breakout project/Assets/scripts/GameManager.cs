using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [HideInInspector] public bool ismusicPlaying = true;
    public TextMeshProUGUI finalScore;
    [SerializeField]private BackgroundMusic  backgroundMusic;
    [SerializeField] private ScoreSystem scoreSystem;

[SerializeField] private IntSO paddleLives;
    [SerializeField] private int maxLives;
    [HideInInspector]public int SceneIndex = 0;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DontDestroyOnLoad(gameObject);
    }

    public void Play() //  causes main game to play
    {
        ScoreSystem.ScoreCount = 0;
        paddleLives.CharacterLives = maxLives;
        SceneIndex = 1;
        ismusicPlaying = true;
        SceneManager.LoadScene(SceneIndex);

    }


    public void Quit() // exits game 
    {
        Application.Quit(0);
    }
    public void End() // transisions to winning scene
    {

       backgroundMusic.StopMusic();
        SceneIndex = 3;
        ismusicPlaying = false;
        SceneManager.LoadScene(SceneIndex);




    }

    public void NextLevel() // transisions to winning scene
    {

        paddleLives.CharacterLives++;
        backgroundMusic.StopMusic();
        SceneIndex = 2;
        ismusicPlaying = true;
        SceneManager.LoadScene(SceneIndex);




    }


    public void ResetGame()
    { // reset to the games title screen
        ScoreSystem.ScoreCount = 0;
        
        backgroundMusic.StopMusic();
        paddleLives.CharacterLives = maxLives;
        SceneIndex = 0;
        SceneManager.LoadScene(SceneIndex);
        ismusicPlaying = true;
        Debug.Log("reset presses i guess");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

