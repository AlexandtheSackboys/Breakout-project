
using TMPro;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI ScoreCountText; //reference to UI in scene
    [HideInInspector] public int scoreCount;
    [SerializeField] private bool hasMusic;
    [SerializeField] private bool isLevel1;
    private GameManager gameManager;
    private BackgroundMusic backgroundMusic = null;
    [SerializeField] private GameObject layerBorder;
    private int maxPoints = 50;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (hasMusic) 
        {
            backgroundMusic = GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();
        }
        UpdateScoreText();
    }

    public void BlockDestroy()
    {
        scoreCount++; // adds a value with to the Score when destroyed
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (ScoreCountText != null)
        {
            ScoreCountText.text = "Score: " + scoreCount.ToString(); // update the Score text when block has Disappeard 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreCount >= maxPoints / 2) 
        {
            Destroy(layerBorder);
        }
        if (isLevel1)
        {
            if (scoreCount == maxPoints)
            {
                backgroundMusic.StopMusic();
                gameManager.NextLevel();

            }
            return; 
        }
        if (scoreCount == maxPoints)
        {
            backgroundMusic.StopMusic();
            gameManager.End();

        }


    }
}

