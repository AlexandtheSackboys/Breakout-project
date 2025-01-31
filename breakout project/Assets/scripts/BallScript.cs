using TMPro;
using UnityEngine;


public class BallScript : MonoBehaviour
{

    public Rigidbody rb;
    public  float Force_X;
    public float Force_Z;
    public GameObject ballPrefab;
    public Transform Spawner;

    public GameObject life_Orbs;
    public float lives = 3f;
    public Transform OrbSpawner;



    [SerializeField] private AudioSource Break;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LifeOrbs();
        // applies forces to the x and z directions
        rb.AddForce (new Vector3 (Force_X, 0, Force_Z));
    }



    private void OnCollisionEnter(Collision Collide)
    {

        if (Collide.gameObject.CompareTag("Deadzone"))
        {


            ballPrefab.transform.position = Spawner.transform.position;
            lives--;
            LifeOrbs();


            if (lives <= 0)
            {

                Destroy(ballPrefab);
            }
        }

        if (Collide.gameObject.CompareTag("Block")) {

           Break.Play();


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
