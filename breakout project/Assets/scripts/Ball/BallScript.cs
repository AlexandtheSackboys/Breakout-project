using TMPro;
using UnityEditor;
using UnityEngine;


public class BallScript : MonoBehaviour
{

    public Rigidbody rb;

    [Range(-25, 25)] public float magnitude_X;
    [Range(10, 25)] public float magnitude_Z;


    public Transform ballSpawner;

    public Powerup item;
    public GameObject DiegeticLives;
    public IntSO Lives;
    public Transform orbSpawner;
    private ScoreSystem itemTrack;

    private PaddleController paddleController;
    private BackgroundMusic backgroundMusic;

    private bool lowHp;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMusic = GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();
        paddleController = GameObject.Find("Player_Paddle").GetComponent<PaddleController>();
        itemTrack = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();




        if (backgroundMusic == null)
        {
            Debug.Log("music is null");
            backgroundMusic.NormalMusic();
        }

        lifeOrbs();
        // applies forces to the x and z directions


        rb.linearVelocity = new Vector3(magnitude_X, 0, magnitude_Z);
        lowHp = false;
    }



    private void OnCollisionEnter(Collision collide)
    {

        if (collide.gameObject.CompareTag("Deadzone"))
        {


            gameObject.transform.position = ballSpawner.transform.position;
            Lives.CharacterLives--;

            DynamicMusic();
            Debug.Log(Lives);

            lifeOrbs();



            rb.linearVelocity = new Vector3(-magnitude_X, 0, magnitude_Z);


            if (Lives.CharacterLives == 1)
            {
                item.Spawn();
            }
            else if (Lives.CharacterLives <= 0)
            {

                Destroy(gameObject);
                backgroundMusic.StopMusic();
                GameManager.Instance.End();
            }








        }
        else if (collide.gameObject.CompareTag("Contingency"))
        {
            rb.linearVelocity = new Vector3(magnitude_X, 0, magnitude_Z);
            gameObject.transform.position = ballSpawner.transform.position;
        }

    }

    public void lifeOrbs()
    {

        // Destroy any existing life orbs to avoid duplicates
        foreach (Transform child in orbSpawner)
        {
            Destroy(child.gameObject);
        }
        // Spawn life orbs based on lives left
        for (int orbNumber = 0; orbNumber < Lives.CharacterLives;orbNumber++)
        {
            // You can position these orbs in different spots around the OrbSpawner
            Instantiate(DiegeticLives, orbSpawner.position - new Vector3(orbNumber * 2, 0, 0), Quaternion.identity, orbSpawner);
        }
    }

    public void lifeIncrease()
    {
        Lives.CharacterLives++;
        DynamicMusic();
        lifeOrbs();
        paddleController.GatherLife = false;

    }

    //this function below deals with when each song is played based on the number of lives the player has
    public void DynamicMusic()
    {
        if (Lives.CharacterLives < 3 && lowHp == false)
        {
            lowHp = true;

            backgroundMusic.LowHealthMusic();
        }
        else if (Lives.CharacterLives > 2 && lowHp == true)
        {
            lowHp = false;

            backgroundMusic.NormalMusic();
        }
    }



}
