using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{

    public Rigidbody rb;
    public float Force_X = 1000f;
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Deadzone")) { 
            ballPrefab.transform.position = Spawner.transform.position;
            lives--;
            if (lives <= 0) { 

            Destroy(ballPrefab);
            }
        }
    }

}
