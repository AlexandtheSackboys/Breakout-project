using UnityEngine;

public class Powerup : MonoBehaviour
{
    private bool isTaken = false;
    //public Paddle_Controller clone_Spawn;
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
}
