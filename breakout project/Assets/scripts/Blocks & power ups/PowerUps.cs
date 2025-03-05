using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private BallScript _ballScript;
    private PaddleController _paddleController;

    void Start()
    {
        _ballScript = GameObject.Find("Ball").GetComponent<BallScript>();
        _paddleController = GameObject.Find("Player_Paddle").GetComponent<PaddleController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            activatePowerUp();

            Destroy(gameObject);
        }
    }




    // the function beklow will spawn an item pill in a random location whre the paddle can moves
    public void Spawn()
    {
        Instantiate(gameObject, new Vector3(Random.Range(-19.76f, 19.76f), 7.35f, -3.08f), Quaternion.identity);

    }
    /* the function below chooses a random number between 0-11
     and will select one of Four power ups to activate */
    private void activatePowerUp()
    {


        if (_paddleController.SpreadActivate == false || _paddleController.AimActive == false || _paddleController.ScaleActive == false || _paddleController.GatherLife == false)
        {

            int powerUp = Random.Range(0, 11);
            Debug.Log("Power up number: " + powerUp);
            if (powerUp >= 1 && powerUp < 4)
            {

                _paddleController.Spread();



            }
            else if (powerUp >= 4 && powerUp < 7)
            {

                _paddleController.AimTime();


            }

            else if (powerUp >= 7 && powerUp < 10)
            {

                _paddleController.ScaleUp();

            }

            else if (powerUp == 10)
            {
                _paddleController.GatherLife = true;
                _ballScript.LifeIncrease();
            }


        }
    }


}
