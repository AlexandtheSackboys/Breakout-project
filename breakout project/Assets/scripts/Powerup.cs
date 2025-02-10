using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private bool isTaken = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Paddle_Controller>().ActivatePowerUp();
            Destroy(gameObject);

        }
    }

    public bool IsTaken()
    {
        return isTaken;
    }

    public void CannotBeAccessed()
    {
        Destroy(gameObject);
    }

    public void Spawn() { 
    Instantiate(gameObject, new Vector3(Random.Range(-19.76f,19.76f), 7.35f, -3.08f), Quaternion.identity);

    }
}
