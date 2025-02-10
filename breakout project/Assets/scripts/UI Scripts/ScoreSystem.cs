using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI ScoreCountText; //reference to UI in scene
    [HideInInspector] public int scoreCount = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

    }
}

