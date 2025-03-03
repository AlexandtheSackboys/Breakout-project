
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _scoreCountText; //reference to UI in scene
    public static int s_ScoreCount;


    private BackgroundMusic _backgroundMusic;
    [SerializeField] private GameObject layerBorder;
    [Range(20,100)][SerializeField]private float maxPoints;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


       _backgroundMusic = GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();
        
        UpdateScoreText();
    }

    public void BlockDestroy()
    {
        s_ScoreCount++; // adds a value with to the Score when destroyed
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (_scoreCountText != null)
        {

                _scoreCountText.text = "Score: " + s_ScoreCount.ToString(); // update the Score text when block has Disappeard 
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (GameManager.s_SceneIndex) {


            case 1:

                firstBoard();
                break;

            case 2:    

                secondBoard();
                break;

                // make them function calls instead of Switch
        }

        
    }

    void firstBoard() 
    {

        if (s_ScoreCount >= maxPoints / 2)
        {
            _backgroundMusic.StopMusic();
            GameManager.s_Instance.NextLevel();

        }
    }

    void secondBoard() 
    {
        if (s_ScoreCount >= maxPoints * 0.75f)
        {
            Destroy(layerBorder);
        }
        if (s_ScoreCount >= maxPoints)
        {

            _backgroundMusic.StopMusic();
            GameManager.s_Instance.End();

        }
    }
}

