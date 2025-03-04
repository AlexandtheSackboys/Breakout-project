using TMPro;
using UnityEditor;
using UnityEngine;


public class BallScript : MonoBehaviour
{

    public Rigidbody rb;

    [SerializeField][Range(-25, 25)] private float _magnitudeX;
    [SerializeField][Range(10, 25)] private float _magnitudeZ;


    [SerializeField] private Transform _ballSpawner, _orbSpawner;


    [SerializeField] private PowerUp _itemPill;
    [SerializeField] private GameObject _diegeticLives;
    public IntSO Lives;


    private PaddleController _paddleController;
    private BackgroundMusic _backgroundMusic;

    private bool _lowHp;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _backgroundMusic = GameObject.Find("BackgroundMusic_emitter").GetComponent<BackgroundMusic>();
        _paddleController = GameObject.Find("Player_Paddle").GetComponent<PaddleController>();





        if (_backgroundMusic == null)
        {
            Debug.Log("music is null");
            _backgroundMusic.NormalMusic();
        }

        lifeOrbs();
        // applies forces to the x and z directions


        rb.linearVelocity = new Vector3(_magnitudeX, 0, _magnitudeZ);
        _lowHp = false;
    }



    private void OnCollisionEnter(Collision collide)
    {

        if (collide.gameObject.CompareTag("Deadzone"))
        {


            gameObject.transform.position = _ballSpawner.transform.position;
            Lives.CharacterLives--;

            dynamicMusic();
            Debug.Log(Lives);

            lifeOrbs();



            rb.linearVelocity = new Vector3(-_magnitudeX, 0, _magnitudeZ);


            if (Lives.CharacterLives == 1)
            {
                _itemPill.Spawn();
            }
            else if (Lives.CharacterLives <= 0)
            {

                Destroy(gameObject);
                _backgroundMusic.StopMusic();
                GameManager.Instance.End();
            }








        }
        else if (collide.gameObject.CompareTag("Contingency"))
        {
            rb.linearVelocity = new Vector3(_magnitudeX, 0, _magnitudeZ);
            gameObject.transform.position = _ballSpawner.transform.position;
        }

    }

    public void lifeOrbs()
    {

        // Destroy any existing life orbs to avoid duplicates
        foreach (Transform child in _orbSpawner)
        {
            Destroy(child.gameObject);
        }
        // Spawn life orbs based on lives left
        for (int orbNumber = 0; orbNumber < Lives.CharacterLives;orbNumber++)
        {
            // You can position these orbs in different spots around the OrbSpawner
            Instantiate(_diegeticLives, _orbSpawner.position - new Vector3(orbNumber * 2, 0, 0), Quaternion.identity, _orbSpawner);
        }
    }

    public void LifeIncrease()
    {
        Lives.CharacterLives++;
        dynamicMusic();
        lifeOrbs();
        _paddleController.GatherLife = false;

    }

    //this function below deals with when each song is played based on the number of lives the player has
    public void dynamicMusic()
    {
        if (Lives.CharacterLives < 3 && _lowHp == false)
        {
            _lowHp = true;

            _backgroundMusic.LowHealthMusic();
        }
        else if (Lives.CharacterLives > 2 && _lowHp == true)
        {
            _lowHp = false;

            _backgroundMusic.NormalMusic();
        }
    }



}
