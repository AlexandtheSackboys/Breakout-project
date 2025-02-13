using FMODUnity;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle_Controller : MonoBehaviour
{
    // stats that can be edited in editor
    [SerializeField][Range(5000,30000)] private float speed;
    [SerializeField][Range(20,30)] private float ball_releaseSpeed;
    [SerializeField][Range(3, 10)] private float timeTilRelease;
    [SerializeField][Range(3, 10)] private float timeTilShrink;
    [SerializeField][Range(0.5f,2.5f)] private float paddle_Extension;
    [SerializeField][Range(2,8)] private float slowDown;

    // effected by player input
    private new Rigidbody rigidbody;
    private Vector2 moveInput;
    private Animator tilt;

    // player object references
    public GameObject perspective_1st;
    public Perspective_Change cameraChange;
    public BallScript lifeOrb_Increase;
    public GameObject ballPrefab;
    public Rigidbody ballRb;
    public GameObject recallPoint;
    public GameObject left_PaddleEnd;
    public GameObject right_PaddleEnd;


    // power up activation 
    [HideInInspector] public bool spreadActivate = false;
    private bool aimActivate = false;
    private bool extendActivate = false;
    [HideInInspector] public bool isAimActive = false;
    private bool isScaleActive = false;
    private bool gather_LifeOrb = false;
    
    // other variables
    public FakerScript temporary;
    [SerializeField] private StudioEventEmitter hit;
    public PauseMenu paused;

    // Timers
    private float aimTimer = 0f;
    private float scaleTimer = 0f;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

            rigidbody.linearVelocity = new Vector3(moveInput.x * speed * Time.deltaTime, 0, 0);
        }
    }

    public void HandleTimers()
    {
        if (!paused.isPaused)
        {
            // Aim Timer
            if (isAimActive && Time.time >= aimTimer)
            {
                Aim_time();
            }

            // Scale Timer
            if (isScaleActive && Time.time >= scaleTimer)
            {
                ScaleUp();
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hit.Play();


        }

        /* if statement for testing         
        if (collision.gameObject.CompareTag("Item"))
        {
            //temporary.Spread();
        //Aim_time();
        //life();
        //ScaleUp();
        }
         */

    }


    public void ActivatePowerUp()
    {


        if (spreadActivate == false || aimActivate == false || extendActivate == false || gather_LifeOrb == false)
        {

            int powerUp = Random.Range(0, 11);
            Debug.Log("Power up number: "+ powerUp);
            if (powerUp >= 1 && powerUp < 4)
            {
                spreadActivate = true;
                temporary.Spread();


            }
            else if (powerUp >= 4 && powerUp < 7)
            {
                aimActivate = true;
                Aim_time();


            }

            else if (powerUp >= 7 && powerUp < 10)
            {
                extendActivate = true;
                ScaleUp();
            }

            else if (powerUp == 10)
            { 
                gather_LifeOrb = true;
                life();
            }


        }
    }

    public void Aim_time()
    {
        if (!isAimActive)
        {
            isAimActive = true;
            aimTimer = Time.time + timeTilRelease; // will end power up when Time.time is equal to aimTimer as Time.time always counts up

            Debug.Log("Time: " + Time.time);
            Debug.Log("Aim Timer: " + aimTimer);

            ballPrefab.transform.position = recallPoint.transform.position;
            ballPrefab.transform.SetParent(recallPoint.transform, true);
            ballRb.constraints = RigidbodyConstraints.FreezeAll; // frezes rigidbody

            cameraChange.Camera.transform.position = perspective_1st.transform.position;
            cameraChange.Camera.transform.rotation = perspective_1st.transform.rotation;
            cameraChange.Camera.transform.SetParent(perspective_1st.transform, true);
            // changes camera perspective closer to the paddle
            return;
        }
        Debug.Log("Aim Power-up Ended");

        ballPrefab.transform.SetParent(null);
        ballRb.constraints = RigidbodyConstraints.FreezePositionY; 

        cameraChange.Camera.transform.SetParent(null);
        cameraChange.Camera.transform.position = cameraChange.perspective_3rd.transform.position;
        cameraChange.Camera.transform.rotation = cameraChange.perspective_3rd.transform.rotation;

        ballRb.linearVelocity = new Vector3(5, 0, ball_releaseSpeed);
        isAimActive = false;
        // causes padddle to return to its original state when Time.time is equal to aimTimer

    }




    // Activate Scale Power-up
    public void ScaleUp()
    {
        if (!isScaleActive)
        {
            isScaleActive = true;
            scaleTimer = Time.time + timeTilShrink; // will end power up when Time.time is equal to scaleTimer as Time.time always counts up
            Debug.Log("Time: " + Time.time);
            Debug.Log("Scale Timer: " + scaleTimer);
            

            left_PaddleEnd.transform.localScale = new Vector3(paddle_Extension, 1.46672726f, 1.54101229f);
            right_PaddleEnd.transform.localScale = new Vector3(paddle_Extension, 1.46672726f, 1.54101229f);
            // changes the length of the edges of the paddle
            speed /= slowDown;
            return;
        }
        
            Debug.Log("Scale Power-up Ended");

            left_PaddleEnd.transform.localScale = new Vector3(0.115734726f, 1.46672726f, 1.54101229f);
            right_PaddleEnd.transform.localScale = new Vector3(0.115734726f, 1.46672726f, 1.54101229f);

            speed *= slowDown;
            isScaleActive = false;
        // causes padddle to return to its original state when Time.time is equal to scaleTimer
    }

    public void life()
    {
        lifeOrb_Increase.lives++;
        lifeOrb_Increase.lifeOrbs();        
        gather_LifeOrb = false;
    }

}

