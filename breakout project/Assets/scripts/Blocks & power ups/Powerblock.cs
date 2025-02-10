using UnityEngine;

public class Powerblock : MonoBehaviour
{
    public float hitpoints;
    private ScoreSystem scoreSystem; // reference game manager

    public Powerup pillSpawner;

    [SerializeField] private AudioSource collect;

    private void Start()
    {
        scoreSystem = FindAnyObjectByType<ScoreSystem>(); // Find the GameManager in the scene
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

            hitpoints--;
            collect.Play();

            if (hitpoints == 0)
            {

                BlockGone();
                pillSpawner.Spawn();

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



    }
}


