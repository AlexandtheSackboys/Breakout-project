using TMPro;
using UnityEngine;


public class BallScript : MonoBehaviour
{

    public Rigidbody rb;

    [Range(-25, 25)] public float magnitude_X;
    [Range(10, 25)] public float magnitude_Z;
    [Range(1,3)] public int temp_hits;
    
    public Transform spawner;

    public Powerup item;
    public GameObject life_Orbs;
    public float lives;
    public Transform orbSpawner;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        lifeOrbs();
        // applies forces to the x and z directions


        rb.linearVelocity = new Vector3(magnitude_X, 0, magnitude_Z);




    }



    private void OnCollisionEnter(Collision collide)
    {

        if (collide.gameObject.CompareTag("Deadzone"))
        {


            gameObject.transform.position = spawner.transform.position;
            lives--;

            Debug.Log(lives);

            lifeOrbs();



            rb.linearVelocity = new Vector3(-magnitude_X, 0, magnitude_Z);


            if (lives <= 2 && lives > 0) 
            { 
                item.Spawn();  
            }
           else if (lives <= 0)
            {

                Destroy(gameObject);
            }





            else if (collide.gameObject.CompareTag("Contingency"))
            {
                rb.linearVelocity = new Vector3(magnitude_X, 0, magnitude_Z);
                gameObject.transform.position = spawner.transform.position;
            }


        } 
    }

        void lifeOrbs()
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
