using System;
using System.Collections;
using UnityEngine;

public class FakerScript : MonoBehaviour
{
    // Fakers are the pink balls that appear when the Spread power up is activated
    public Rigidbody FakerRb;

    [Range(-1250, 1250)] public float MagnitudeX;
    [Range(-1250, 1250)] public float MagnitudeZ;
    
    // determines how many hits a Faker ball can have before being destroyed
    [SerializeField][Range(1, 5)] private int _tempHits;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // applies forces to the x and z directions

        FakerRb.AddForce(MagnitudeX, 0, MagnitudeZ);




    }



    private void OnCollisionEnter(Collision collide)
    {

        if (collide.gameObject.CompareTag("Deadzone"))
        {

            Destroy(gameObject);

        }

        else if (collide.gameObject.CompareTag("Contingency"))
        {
            FakerRb.linearVelocity = new Vector3(MagnitudeX, 0, MagnitudeZ);
            // resets the velocity of the Faker balls if they get out of bounds of the board

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

