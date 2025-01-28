using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI ScoreCountText; //reference to UI in scene
    private int ScoreCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    }
}

