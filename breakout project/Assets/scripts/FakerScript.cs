using UnityEngine;

public class FakerScript : MonoBehaviour
{

    public Rigidbody rb;

    [Range(-25, 25)] public float magnitude_X;
    [Range(10, 25)] public float magnitude_Z;
    [Range(1, 5)] public int temp_hits;
    public Paddle_Controller player;
   // public Transform destination;




   // public Transform spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // applies forces to the x and z directions
       
        rb.linearVelocity = new Vector3(magnitude_X, 0, magnitude_Z);




    }



    private void OnCollisionEnter(Collision collide)
    {

        if (collide.gameObject.CompareTag("Deadzone"))
        {



            Destroy(gameObject);

        }

        else if (collide.gameObject.CompareTag("Contingency"))
        {
            rb.linearVelocity = new Vector3(magnitude_X, 0, magnitude_Z);

        }

        else if (collide.gameObject.CompareTag("Block"))
        {
            temp_hits--;

            if (temp_hits <= 0)
            {

                Destroy(gameObject);
            }
        }
    }
    public void Split()
    {
        

        Rigidbody fakerRb = Instantiate(rb, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
        fakerRb.AddForce(Random.onUnitSphere * 25.0f, ForceMode.Impulse);
        
        Rigidbody fakerRb2 = Instantiate(rb, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
        fakerRb2.AddForce(Random.onUnitSphere * -25.0f, ForceMode.Impulse);


        player.splitActivate = false;


    }
}