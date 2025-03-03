using System;
using System.Collections;
using UnityEngine;

public class FakerScript : MonoBehaviour
{
    // Fakers are the pink ball that appear when the Spread power up is activated
    public Rigidbody FakerRb;

    [Range(-1250, 1250)] public float Magnitude_X;
    [Range(-1250, 1250)] public float Magnitude_Z;
    [Range(1, 5)] private int _tempHits;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // applies forces to the x and z directions

        FakerRb.AddForce(Magnitude_X, 0, Magnitude_Z);




    }



    private void OnCollisionEnter(Collision collide)
    {

        if (collide.gameObject.CompareTag("Deadzone"))
        {



            Destroy(gameObject);

        }

        else if (collide.gameObject.CompareTag("Contingency"))
        {
            FakerRb.linearVelocity = new Vector3(Magnitude_X, 0, Magnitude_Z);

        }

        else if (collide.gameObject.CompareTag("Block"))
        {
            _tempHits--;

            if (_tempHits <= 0)
            {

                Destroy(gameObject);

            }
        }
    }



}

