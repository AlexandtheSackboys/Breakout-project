
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _scoreCountText; //reference to UI in scene
    public static int ScoreCount;


    private BackgroundMusic _backgroundMusic;
    [SerializeField] private GameObject _layerBorder;
    [Range(20,100)][SerializeField]private float _maxPoints;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


       _backgroundMusic = GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();
        
        UpdateScoreText();
    }

    public void BlockDestroy()
    {
        ScoreCount++; // adds a value with to the Score when destroyed
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (_scoreCountText != null)
        {

                _scoreCountText.text = "Score: " + ScoreCount.ToString(); // update the Score text when block has Disappeard 
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (GameManager.SceneIndex) {


            case 1:

                firstBoard();
                break;

            case 2:    

                secondBoard();
                break;


        }

        
    }

    void firstBoard() 
    {
        //makes sure when the player gets max points it moves on to the next board
        if (ScoreCount >= _maxPoints / 2)
        {
            _backgroundMusic.StopMusic();
            GameManager.Instance.NextLevel();

        }
    }

    void secondBoard() 
    {
        //makes sure when the player gets max points it moves on to the Game over scene
        if (ScoreCount >= _maxPoints * 0.75f)
        {
            Destroy(_layerBorder);
        }
        if (ScoreCount >= _maxPoints)
        {

            _backgroundMusic.StopMusic();
            GameManager.Instance.End();

        }
    }
}

