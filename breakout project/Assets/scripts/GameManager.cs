using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [HideInInspector] public bool ismusicPlaying = true;
    public ScoreSystem score;
    public TextMeshProUGUI finalScore;
    public backgroundMusic music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Play() //  causes main game to play
    {
        music.StopMusic();
        SceneManager.LoadScene(0);
        music.NormalMusic();
    }


    public void Quit() // exits game 
    {
        Application.Quit(0);
    }
    public void End() // transisions to winning scene
    {
        finalScore.text = " Your Score: " + score.scoreCount.ToString(); // update the Score text when block has Disappeard 
        music.StopMusic();
        SceneManager.LoadScene(1);
        ismusicPlaying = false;



    }

    public void ResetGame()
    { // reset to the games title screen
        music.StopMusic();
        SceneManager.LoadScene(0);
        ismusicPlaying = true;
        Debug.Log("reset presses i guess");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

