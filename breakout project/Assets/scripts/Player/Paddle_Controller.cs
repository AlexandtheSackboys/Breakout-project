using FMODUnity;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    // stats that can be edited in editor
    [SerializeField][Range(5000, 30000)] private float paddleSpeed;
    [SerializeField][Range(20, 30)] private float ball_releaseSpeed;

    [SerializeField][Range(3, 10)] private float timeRelease;
    [SerializeField][Range(3, 10)] private float timeShrink;
    [SerializeField][Range(1,5)]  private float spreadLifetime;

    [SerializeField][Range(0.5f, 2.5f)] private float paddle_Extension;
    [SerializeField][Range(2, 8)] private float slowDown;

    // effected by player input
    private new Rigidbody rigidbody;
    private Vector2 moveInput;
    private Animator tilt;

    // player object references
    [SerializeField] private GameObject perspectivePowerUp;

    private Perspective_Change cameraChange;
    private BallScript ballScript;
    
    [SerializeField]private GameObject ballPrefab;
    [SerializeField]private Rigidbody ballRb;

    [SerializeField] private GameObject recallPoint;
    [SerializeField] private GameObject left_PaddleEnd;
    [SerializeField] private GameObject right_PaddleEnd;


    // power up activation 
    [HideInInspector] public bool SpreadActivate = false;
    [HideInInspector] public bool AimActive = false;
    private bool scaleActive = false;
    [HideInInspector] public bool GatherLife = false;

    // other variables
    [SerializeField] private FakerScript temporaryBalls;
    [SerializeField] private StudioEventEmitter paddleHit;
    private PauseMenu paused;

    // Power Up texxt objects
    [SerializeField] private GameObject aimText;
    [SerializeField] private GameObject extendText;
    [SerializeField] private GameObject spreadText;


    // Timers
    private float aimTimer = 0f;
    private float scaleTimer = 0f;
    [HideInInspector] public float TextShow = 0f;
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraChange = GameObject.Find("Main Camera").GetComponent<Perspective_Change>();
        ballScript = GameObject.Find("Ball").GetComponent<BallScript>();
        paused = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        rigidbody = GetComponent<Rigidbody>();
        tilt = GetComponent<Animator>();


    }

    // Update is called once per frame   
    void Update()
    {
        MovementHandler();
        HandleTimers();







    }

    // function below deals with animation and movement of paddle 
    private void MovementHandler()
    {
        if (tilt != null)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                tilt.SetTrigger("TiltR_Open");

            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                tilt.SetTrigger("TiltR_Close");

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                tilt.SetTrigger("TiltL_Open");

            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                tilt.SetTrigger("TiltL_Close");

            rigidbody.linearVelocity = new Vector3(moveInput.x * paddleSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void HandleTimers()
    {
        if (!paused.isPaused)
        {
            // Aim Timer
            if (AimActive && Time.time >= aimTimer)
            {
                AimTime();
            }

            // Scale Timer
            if (scaleActive && Time.time >= scaleTimer)
            {
                ScaleUp();
            }
            if (SpreadActivate && Time.time >= TextShow)
            {
                Spread();

            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            paddleHit.Play();


        }
    }


    public void ActivatePowerUp()
    {


        if (SpreadActivate == false || AimActive == false || scaleActive == false || GatherLife == false)
        {

            int powerUp = Random.Range(0,11);
            Debug.Log("Power up number: " + powerUp);
            if (powerUp >= 1 && powerUp < 4)
            {

                Spread();



            }
            else if (powerUp >= 4 && powerUp < 7)
            {

                AimTime();


            }

            else if (powerUp >= 7 && powerUp < 10)
            {

                ScaleUp();

            }

            else if (powerUp == 10)
            {
                GatherLife = true;
                ballScript.lifeIncrease();
            }


        }
    }

    public void AimTime()
    {
        if (!AimActive)
        {
            AimActive = true;
            aimTimer = Time.time + timeRelease; // will end power up when Time.time is equal to aimTimer as Time.time always counts up

            Debug.Log("Time: " + Time.time);
            Debug.Log("Aim Timer: " + aimTimer);

            ballPrefab.transform.position = recallPoint.transform.position;
            ballPrefab.transform.SetParent(recallPoint.transform, true);
            ballRb.constraints = RigidbodyConstraints.FreezeAll; // freezes rigidbody
            aimText.SetActive(true);


            cameraChange.CameraObject.transform.position = perspectivePowerUp.transform.position;
            cameraChange.CameraObject.transform.rotation = perspectivePowerUp.transform.rotation;
            cameraChange.CameraObject.transform.SetParent(perspectivePowerUp.transform, true);
            // changes camera perspective closer to the paddle
            return;
        }
        Debug.Log("Aim Power-up Ended");

        aimText.SetActive(false);
        ballPrefab.transform.SetParent(null);
        ballRb.constraints = RigidbodyConstraints.FreezePositionY;

        cameraChange.CameraObject.transform.SetParent(null);
        cameraChange.CameraObject.transform.position = cameraChange.Perspective_3rd.transform.position;
        cameraChange.CameraObject.transform.rotation = cameraChange.Perspective_3rd.transform.rotation;

        ballRb.linearVelocity = new Vector3(5, 0, ball_releaseSpeed);
        AimActive = false;
        // causes padddle to return to its original state when Time.time is equal to aimTimer

    }




    // Activate Scale Power-up
    public void ScaleUp()
    {
        if (!scaleActive)
        {
            scaleActive = true;
            scaleTimer = Time.time + timeShrink; // will end power up when Time.time is equal to scaleTimer as Time.time always counts up
            Debug.Log("Time: " + Time.time);
            Debug.Log("Scale Timer: " + scaleTimer);
            extendText.SetActive(true);

            left_PaddleEnd.transform.localScale = new Vector3(paddle_Extension, 1.46672726f, 1.54101229f);
            right_PaddleEnd.transform.localScale = new Vector3(paddle_Extension, 1.46672726f, 1.54101229f);
            // changes the length of the edges of the paddle

            paddleSpeed /= slowDown;
            return;
        }

        Debug.Log("Scale Power-up Ended");
        extendText.SetActive(false);
        left_PaddleEnd.transform.localScale = new Vector3(0.115734726f, 1.46672726f, 1.54101229f);
        right_PaddleEnd.transform.localScale = new Vector3(0.115734726f, 1.46672726f, 1.54101229f);

        paddleSpeed *= slowDown;
        scaleActive = false;
        // causes padddle to return to its original state when Time.time is equal to scaleTimer
    }

    public void Spread()
    {

        if (!SpreadActivate)
        {
            SpreadActivate = true;
            TextShow = Time.time + spreadLifetime;
            spreadText.SetActive(true);
            Rigidbody fakerRb = Instantiate(temporaryBalls.rb, new Vector3(gameObject.transform.position.x, ballPrefab.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
            fakerRb.AddForce(temporaryBalls.Magnitude_X, 0, temporaryBalls.Magnitude_Z);



            Rigidbody fakerRb2 = Instantiate(temporaryBalls.rb, new Vector3(gameObject.transform.position.x, ballPrefab.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
            fakerRb2.AddForce(-temporaryBalls.Magnitude_X, 0, temporaryBalls.Magnitude_Z);
            // produces 2 temporary balls which have a limited number of times that they can hit a block
            return;

        }

        spreadText.SetActive(false);
        SpreadActivate = false;


    }
}

