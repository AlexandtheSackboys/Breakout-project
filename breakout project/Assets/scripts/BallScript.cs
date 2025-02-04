using TMPro;
using UnityEngine;


public class BallScript : MonoBehaviour
{

    public Rigidbody rb;

    [Range(-1250,1250)]public  float force_X;
    [Range(-1250, 1250)] public float force_Z;
    [Range(0, 10)] public float bounceBack; // adds power to each end of the paddle



    public Transform spawner;



    public GameObject life_Orbs;
    public float lives;
    public Transform orbSpawner;





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


            gameObject.transform.position = spawner.transform.position;
            lives--;

            Debug.Log(lives);

            LifeOrbs();



            rb.AddForce(-force_X/8, 0, force_Z/8);



            if (lives <= 0)
            {

                Destroy(gameObject);
            }

        }

        else if (collide.gameObject.CompareTag("Block"))
        {

            rb.AddForce(bounceBack / 4, 0, bounceBack/ 4);



        }



        else if (collide.gameObject.CompareTag("Barrier"))  
        { 
            rb.AddForce(-bounceBack, 0, -bounceBack); 
        }

        else if (collide.gameObject.CompareTag("Contingency"))
        {

            gameObject.transform.position = spawner.transform.position;
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
