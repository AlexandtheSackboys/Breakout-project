using UnityEngine;

public class PowerUp : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PaddleController>().ActivatePowerUp(); 
            // makes reference to the ActivatePowerUp function within PaddleController class
            Destroy(gameObject);
        }
    }




    // the function beklow will spawn an item pill in a random location whre the paddle can moves
    public void Spawn()
    {
        Instantiate(gameObject, new Vector3(Random.Range(-19.76f, 19.76f), 7.35f, -3.08f), Quaternion.identity);

    }


}
