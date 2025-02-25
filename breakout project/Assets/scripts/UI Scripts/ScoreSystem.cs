
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI ScoreCountText; //reference to UI in scene
    [HideInInspector] public static int ScoreCount;

    [HideInInspector] public bool GameOver = false;
    private BackgroundMusic backgroundMusic;
    [SerializeField] private GameObject layerBorder;
    [Range(20,100)][SerializeField]private float maxPoints;
    private GameManager gameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

       backgroundMusic = GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();
        
        UpdateScoreText();
    }

    public void BlockDestroy()
    {
        ScoreCount++; // adds a value with to the Score when destroyed
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (ScoreCountText != null)
        {

                ScoreCountText.text = "Score: " + ScoreCount.ToString(); // update the Score text when block has Disappeard 
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (gameManager.SceneIndex) {


            case 1:

                if (ScoreCount == maxPoints / 2)
                {
                    backgroundMusic.StopMusic();
                    GameManager.Instance.NextLevel();

                }
                break;

            case 2:    

                if (ScoreCount == maxPoints * 0.75f)
                {
                    Destroy(layerBorder);
                }
                if (ScoreCount == maxPoints)
                {

                    backgroundMusic.StopMusic();
                    GameManager.Instance.End();

                }
                break;


        }
    } 
}

