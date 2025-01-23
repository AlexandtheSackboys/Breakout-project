using UnityEngine;
using System.Collections;
public class blockWall : MonoBehaviour
{
    public float hitpoints = 1f;
    public GameObject Projectile;
    private GameManager gameManager; // reference game manager

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>(); // Find the GameManager in the scene
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
        if (gameManager != null)
        {
            gameManager.BlockDestroy(); // Notify GameManager that an enemy is killed
        }
        Destroy(gameObject); // game object is no longer in scene
    }
}
