using System;
using System.Collections;
using UnityEngine;

public class FakerScript : MonoBehaviour
{

    public Rigidbody rb;

    [Range(-1250, 1250)] public float Magnitude_X;
    [Range(-1250, 1250)] public float Magnitude_Z;
    [Range(1, 5)] private int tempHits;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // applies forces to the x and z directions

        rb.AddForce(Magnitude_X, 0, Magnitude_Z);




    }



    private void OnCollisionEnter(Collision collide)
    {

        if (collide.gameObject.CompareTag("Deadzone"))
        {



            Destroy(gameObject);

        }

        else if (collide.gameObject.CompareTag("Contingency"))
        {
            rb.linearVelocity = new Vector3(Magnitude_X, 0, Magnitude_Z);

        }

        else if (collide.gameObject.CompareTag("Block"))
        {
            tempHits--;

            if (tempHits <= 0)
            {

                Destroy(gameObject);

            }
        }
    }



}

