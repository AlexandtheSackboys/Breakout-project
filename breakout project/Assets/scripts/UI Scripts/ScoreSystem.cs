
using TMPro;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI ScoreCountText; //reference to UI in scene
    [HideInInspector] public static int ScoreCount;
    [SerializeField] private bool hasMusic;
    [SerializeField] private bool isLevel1;

    private BackgroundMusic backgroundMusic = null;
    [SerializeField] private GameObject layerBorder;
    private float maxPoints = 20;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (hasMusic) 
        {
            backgroundMusic = GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();
        }
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
 
        if (isLevel1)
        {
            if (ScoreCount == maxPoints/2)
            {
                backgroundMusic.StopMusic();
                GameManager.Instance.NextLevel();

            }
            return; 
        }
        if (ScoreCount >= maxPoints * 0.75f)
        {
            Destroy(layerBorder);
        }
        if (ScoreCount == maxPoints)
        {
            backgroundMusic.StopMusic();
            GameManager.Instance.End();

        }


    }
}

