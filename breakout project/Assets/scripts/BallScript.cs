using TMPro;
using UnityEngine;


public class BallScript : MonoBehaviour
{

    public Rigidbody rb;

    public  float force_X;
    public float force_Z;



    public GameObject ballPrefab;
    public Transform Spawner;

    public float endPower; // adds power to each end of the paddle

    public GameObject life_Orbs;
    public float lives = 3f;
    public Transform OrbSpawner;



    [SerializeField] private AudioSource Break;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LifeOrbs();


        // applies forces to the x and z directions


        rb.AddForce(force_X, 0, force_Z);




    }



    private void OnCollisionEnter(Collision Collide)
    {

        if (Collide.gameObject.CompareTag("Deadzone"))
        {

            force_X = Random.Range(0, 1);
            force_Z = Random.Range(0, 1);

            ballPrefab.transform.position = Spawner.transform.position;
            lives--;

            Debug.Log(lives);

            LifeOrbs();



                rb.AddForce(-force_X, 0, force_Z);





        }

        if (Collide.gameObject.CompareTag("Block")) 
        {

            Break.Play();


        }

        else if (Collide.gameObject.CompareTag("LeftEnd"))
        {

            rb.AddForce(-force_X + endPower, 0, force_Z+ endPower);
        }

        else if (Collide.gameObject.CompareTag("RightEnd"))
        {
            rb.AddForce(force_X + endPower, 0, force_Z + endPower);

        }
        if (lives <= 0)
        {

            Destroy(ballPrefab);
        }


    }

    void LifeOrbs() {

        // Destroy any existing life orbs to avoid duplicates
        foreach (Transform child in OrbSpawner)
        {
            Destroy(child.gameObject);
        }
        // Spawn life orbs based on lives left
        for (int i = 0; i < lives; i++)
        {
            // You can position these orbs in different spots around the OrbSpawner
            Instantiate(life_Orbs, OrbSpawner.position - new Vector3(i*2, 0, 0), Quaternion.identity, OrbSpawner);
        }
    }
}
