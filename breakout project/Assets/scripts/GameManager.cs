using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Play() //  causes main game to play
    {
        SceneManager.LoadScene(1);
    }


    public void Quit() // exits game 
    {
        Application.Quit(0);
    }
    public void Win() // transisions to winning scene
    {
        SceneManager.LoadScene(2);



    }

    public void ResetGame()
    { // reset to the games title screen
        SceneManager.LoadScene(0);
        Debug.Log("reset presses i guess");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

