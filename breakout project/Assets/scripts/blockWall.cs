using UnityEngine;
using System.Collections;
public class blockWall : MonoBehaviour
{
    public float hitpoints;
    public GameObject Projectile;
    private ScoreSystem scoreSystem; // reference game manager


    private void Start()
    {
        scoreSystem = FindAnyObjectByType<ScoreSystem>(); // Find the GameManager in the scene
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

            hitpoints--;

            if (hitpoints == 0) {
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
    }
}
