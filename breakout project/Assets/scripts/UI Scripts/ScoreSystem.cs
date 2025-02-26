
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI ScoreCountText; //reference to UI in scene
    [HideInInspector] public static int ScoreCount;

    private BackgroundMusic backgroundMusic;
    [SerializeField] private GameObject layerBorder;
    [Range(20,100)][SerializeField]private float maxPoints;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


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

        switch (GameManager.SceneIndex) {


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

        if (ScoreCount >= maxPoints / 2)
        {
            backgroundMusic.StopMusic();
            GameManager.Instance.NextLevel();

        }
    }

    void secondBoard() 
    {
        if (ScoreCount == maxPoints * 0.75f)
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

