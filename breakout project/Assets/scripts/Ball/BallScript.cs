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
    public float Lives;
    public Transform orbSpawner;
    private ScoreSystem itemTrack;
    public GameManager sceneChange;
    private PaddleController paddleController;
    private BackgroundMusic music;

    private bool lowHp;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        music = GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();
        paddleController = GameObject.Find("Player_Paddle").GetComponent<PaddleController>();
        itemTrack = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();




        if (music == null)
        {
            Debug.Log("music is null");
            music.NormalMusic();
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
            Lives--;

            DynamicMusic();
            Debug.Log(Lives);

            lifeOrbs();



            rb.linearVelocity = new Vector3(-magnitude_X, 0, magnitude_Z);


            if (Lives == 1)
            {
                item.Spawn();
            }
            else if (Lives <= 0)
            {

                Destroy(gameObject);
                music.StopMusic();
                sceneChange.End();
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
        for (int i = 0; i < Lives; i++)
        {
            // You can position these orbs in different spots around the OrbSpawner
            Instantiate(DiegeticLives, orbSpawner.position - new Vector3(i * 2, 0, 0), Quaternion.identity, orbSpawner);
        }
    }

    public void lifeIncrease()
    {
        Lives++;
        DynamicMusic();
        lifeOrbs();
        paddleController.GatherLife = false;

    }

    //this function below deals with when each song is played based on the number of lives the player has
    public void DynamicMusic()
    {
        if (Lives < 3 && lowHp == false)
        {
            lowHp = true;

            music.LowHealthMusic();
        }
        else if (Lives > 2 && lowHp == true)
        {
            lowHp = false;

            music.NormalMusic();
        }
    }



}
