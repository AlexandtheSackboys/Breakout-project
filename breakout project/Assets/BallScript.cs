using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{

    public Rigidbody rb;
    public float Force_X = 1000f;
    public float Force_Z = 1000f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // applies forces to the x and z directions
        rb.AddForce (new Vector3 (Force_X, 0, Force_Z));
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
