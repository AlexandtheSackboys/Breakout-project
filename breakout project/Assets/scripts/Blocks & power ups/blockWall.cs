using UnityEngine;
using System.Collections;
using FMODUnity;
public class blockWall : MonoBehaviour
{
    public float hitpoints;
    private ScoreSystem scoreSystem; // reference game manager
    public GameObject debris;

    [SerializeField]private StudioEventEmitter Break;

    private void Start()
    {
        scoreSystem = FindAnyObjectByType<ScoreSystem>(); // Find the GameManager in the scene
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

            hitpoints--;
            Break.Play();

            if (hitpoints == 0) {
                debris.transform.position = gameObject.transform.position;
                BlockGone();


            }
        }

    }

    private void BlockGone()
    {
        if (scoreSystem != null)
        {
            scoreSystem.BlockDestroy(); // Notify GameManager that an enemy is killed

        }
        Destroy(gameObject); // game object is no longer in scene
        Instantiate(debris);


    }

}
