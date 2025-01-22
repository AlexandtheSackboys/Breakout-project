using UnityEngine;
using System.Collections;
public class blockWall : MonoBehaviour
{
    public float hitpoints = 1f;
    public GameObject Projectile;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hitpoints--;

            if (hitpoints == 0) { 
                Destroy(gameObject);
            }
        }
        
        


    }
}
