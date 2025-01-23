using TMPro;
using UnityEngine;


public class BallScript : MonoBehaviour
{

    public Rigidbody rb;
    public  float Force_X = 1000f;
    public float Force_Z = 1000f;
     public GameObject ballPrefab;
     public Transform Spawner;

    
    public float lives = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // applies forces to the x and z directions
        rb.AddForce (new Vector3 (Force_X, 0, Force_Z));
    }



    private void OnCollisionEnter(Collision Collide)
    {

        if (Collide.gameObject.CompareTag("Deadzone"))
        {


            ballPrefab.transform.position = Spawner.transform.position;
            lives--;


            if (lives <= 0)
            {

                Destroy(ballPrefab);
            }
        }

        if (Collide.gameObject.CompareTag("Block")) {

            rb.AddForce(new Vector3(Force_X, 0, -Force_Z));

        }
        if (Collide.gameObject.CompareTag("Block2"))
        {

            rb.AddForce(new Vector3(Force_X, 0, Force_Z));

        }


    }
}
