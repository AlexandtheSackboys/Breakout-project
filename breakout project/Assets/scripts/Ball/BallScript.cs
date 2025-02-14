using TMPro;
using UnityEngine;


public class BallScript : MonoBehaviour
{

    public Rigidbody rb;

    [Range(-25, 25)] public float magnitude_X;
    [Range(10, 25)] public float magnitude_Z;

    
    public Transform spawner;

    public Powerup item;
    public GameObject life_Orbs;
    public float lives;
    public Transform orbSpawner;
    private ScoreSystem itemTrack;
    public GameManager sceneChange;
    private Paddle_Controller paddleController;
    private backgroundMusic music;
    private bool lowHp;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        music = GameObject.Find("BackgroundMusic_emitter").GetComponent<backgroundMusic>();
        paddleController = GameObject.Find("Player_Paddle").GetComponent<Paddle_Controller>();
        itemTrack = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
        
        if (music == null )
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


            gameObject.transform.position = spawner.transform.position;
            lives--;

            DynamicMusic();
            Debug.Log(lives);

            lifeOrbs();



            rb.linearVelocity = new Vector3(-magnitude_X, 0, magnitude_Z);


            if (lives == 1)
            {
                item.Spawn();
            }
            else if (lives <= 0)
            {

                Destroy(gameObject);
                music.StopMusic();
                sceneChange.End();
            }





            else if (collide.gameObject.CompareTag("Contingency"))
            {
                rb.linearVelocity = new Vector3(magnitude_X, 0, magnitude_Z);
                gameObject.transform.position = spawner.transform.position;
            }




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
            for (int i = 0; i < lives; i++)
            {
                // You can position these orbs in different spots around the OrbSpawner
                Instantiate(life_Orbs, orbSpawner.position - new Vector3(i * 2, 0, 0), Quaternion.identity, orbSpawner);
            }
        }

    public void life_Increase()
    {
        lives++;
        DynamicMusic();
        lifeOrbs();
        paddleController.gather_LifeOrb = false;

    }

    //this function below deals with when each song is played based on the number of lives the player has
    public void DynamicMusic() 
    {
        if (lives < 3 && lowHp == false)
        {
            lowHp = true;

            music.LowHealthMusic();
        }
        else if (lives > 2 && lowHp == true)
        {
            lowHp = false;

            music.NormalMusic();
        }
    }




}
