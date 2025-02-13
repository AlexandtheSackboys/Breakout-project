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
    public ScoreSystem itemTrack;
    public GameManager sceneChange;
    private backgroundMusic music;
    private bool lowHp;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        music = GameObject.Find("BackgroundMusic_emitter").GetComponent<backgroundMusic>();
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
            if (lives < 3 &&!lowHp)
            {
                lowHp = true;
                music.LowHealthMusic();
            }
            else if (lives > 2 && lowHp)
            {
                lowHp = false;
                music.NormalMusic();
            }

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



    


}
