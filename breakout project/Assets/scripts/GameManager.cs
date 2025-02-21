using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [HideInInspector] public bool ismusicPlaying = true;
    public TextMeshProUGUI finalScore;
    public BackgroundMusic music;
    [SerializeField] private IntSO paddleLives;
    [SerializeField] private int maxLives;
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
        music.StopMusic();
        paddleLives.CharacterLives = maxLives;
        SceneManager.LoadScene(1);
        ismusicPlaying = true;
    }


    public void Quit() // exits game 
    {
        Application.Quit(0);
    }
    public void End() // transisions to winning scene
    {
        finalScore.text = " Your Score: " + ScoreSystem.ScoreCount.ToString(); // update the Score text when block has Disappeard 
        music.StopMusic();
        ismusicPlaying = false;
        SceneManager.LoadScene(3);




    }

    public void NextLevel() // transisions to winning scene
    {
        finalScore.text = " Your Score: " + ScoreSystem.ScoreCount.ToString(); // update the Score text when block has Disappeard 

        music.StopMusic();
        ismusicPlaying = false;
        SceneManager.LoadScene(2);




    }


    public void ResetGame()
    { // reset to the games title screen
        music.StopMusic();
        paddleLives.CharacterLives = maxLives;
        SceneManager.LoadScene(0);
        ismusicPlaying = true;
        Debug.Log("reset presses i guess");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

