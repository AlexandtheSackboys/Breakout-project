using TMPro;
using UnityEngine;


public class BallScript : MonoBehaviour
{

    public Rigidbody rb;

    public  float force_X;
    public float force_Z;



    public GameObject ballPrefab;
    public Transform spawner;

    public float bounceBack; // adds power to each end of the paddle

    public GameObject life_Orbs;
    public float lives;
    public Transform orbSpawner;



    [SerializeField] private AudioSource Break;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LifeOrbs();


        // applies forces to the x and z directions


        rb.AddForce(force_X, 0, force_Z);




    }



    private void OnCollisionEnter(Collision collide)
    {

        if (collide.gameObject.CompareTag("Deadzone"))
        {


            ballPrefab.transform.position = spawner.transform.position;
            lives--;

            Debug.Log(lives);

            LifeOrbs();



            rb.AddForce(-force_X/4, 0, force_Z/4);



            if (lives <= 0)
            {

                Destroy(ballPrefab);
            }

        }

        else if (collide.gameObject.CompareTag("Block"))
        {

            rb.AddForce(bounceBack / 4, 0, bounceBack/ 4);
            Break.Play();


        }



        else if (collide.gameObject.CompareTag("Barrier"))  
        { 
            rb.AddForce(-bounceBack, 0, -bounceBack); 
        }



    }

    void LifeOrbs() {

        // Destroy any existing life orbs to avoid duplicates
        foreach (Transform child in orbSpawner)
        {
            Destroy(child.gameObject);
        }
        // Spawn life orbs based on lives left
        for (int i = 0; i < lives; i++)
        {
            // You can position these orbs in different spots around the OrbSpawner
            Instantiate(life_Orbs, orbSpawner.position - new Vector3(i*2, 0, 0), Quaternion.identity, orbSpawner);
        }
    }
}
