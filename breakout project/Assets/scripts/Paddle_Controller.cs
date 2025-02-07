using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle_Controller : MonoBehaviour
{
    [SerializeField][Range(5000,30000)] private float speed;
    [SerializeField][Range(20,30)] private float ball_releaseSpeed;
    [SerializeField][Range(3, 10)] private float timeTilRelease;
    [SerializeField][Range(0.5f,2.5f)] private float paddle_Extension;
    [SerializeField][Range(2,8)] private float slowDown;

    private new Rigidbody rigidbody;
    private Vector2 moveInput;
    private Animator tilt;


    public GameObject perspective_1st;
    public Perspective_Change cameraChange;
    public GameObject ballPrefab;
    public Rigidbody ballRb;
    public GameObject recallPoint;

    public GameObject left_PaddleEnd;
    public GameObject right_PaddleEnd;


    [HideInInspector] public bool splitActivate = false;


    public FakerScript temporary;

    [SerializeField] private AudioSource hit;
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

        
        if (tilt != null)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                    tilt.SetTrigger("TiltR_Open");

            } // rotates the paddle in the right direction

            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) {
                    tilt.SetTrigger("TiltR_Close");

            } // returns paddle to original rotation from the right tilt animation



            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                    tilt.SetTrigger("TiltL_Open");

            } // rotates the paddle in the left direction

            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                    tilt.SetTrigger("TiltL_Close");

            } // returns paddle to original rotation from the left tilt animation

        }
        rigidbody.linearVelocity = new Vector3(moveInput.x * speed * Time.deltaTime, 0, 0);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hit.Play();


        }

        else if (collision.gameObject.CompareTag("Item"))
        {

            StartCoroutine(scaleUp());
            //StartCoroutine(Aim_time());

            //temporary.Spread();

        }
    }
    public void ActivatePowerUp()
    {
        Debug.Log("Split ball status" + splitActivate);

        if (splitActivate == false)
        {
            int powerUp = Random.Range(1, 3);
            Debug.Log(powerUp);
            if (powerUp == 1)
            {
                splitActivate = true;
                //powerup.text = "Rapid Fire";
            }

        }
    }

    public IEnumerator Aim_time()
    {
        // recallpoint will be the ballSpawner child GameObject
        ballPrefab.transform.position = recallPoint.transform.position;

        ballPrefab.transform.SetParent(recallPoint.transform,true); // makes the gameobject a child of the refrenced game object
        
        ballRb.constraints = RigidbodyConstraints.FreezePositionX 
            | RigidbodyConstraints.FreezePositionY 
            | RigidbodyConstraints.FreezePositionZ; //causes the rigidbodys position in all 3 directions to freeze
        
        cameraChange.Camera.transform.position = perspective_1st.transform.position;

        cameraChange.Camera.transform.rotation = perspective_1st.transform.rotation;

        cameraChange.Camera.transform.SetParent(perspective_1st.transform, true); // makes the gameobject a child of the refrenced game object
        yield return new WaitForSecondsRealtime(timeTilRelease);

        Debug.Log("Recall");

        ballPrefab.transform.SetParent(null); // sperate child and parent game object

        ballRb.constraints = RigidbodyConstraints.FreezePositionY; //causes the rigidbodys position in the y directions to freeze

        cameraChange.Camera.transform.SetParent(null); // sperate child and parent game object

        cameraChange.Camera.transform.position = cameraChange.Perspective_3rd.transform.position;

        cameraChange.Camera.transform.rotation = cameraChange.Perspective_3rd.transform.rotation;

        ballRb.linearVelocity = new Vector3(5, 0, ball_releaseSpeed);
        StopCoroutine(Aim_time());
        


    }

    public IEnumerator scaleUp()
    {
        Debug.Log("Scaling");
        left_PaddleEnd.transform.localScale = new Vector3(paddle_Extension, 1.46672726f, 1.54101229f);
        right_PaddleEnd.transform.localScale = new Vector3(paddle_Extension, 1.46672726f, 1.54101229f);
        
        speed = speed / slowDown;
        rigidbody.linearVelocity = new Vector3(moveInput.x * speed *  Time.deltaTime, 0, 0);
        
        yield return new WaitForSecondsRealtime(10);

        left_PaddleEnd.transform.localScale = new Vector3(0.115734726f, 1.46672726f, 1.54101229f);
        right_PaddleEnd.transform.localScale = new Vector3(0.115734726f, 1.46672726f, 1.54101229f);
        speed = speed * slowDown;
        rigidbody.linearVelocity = new Vector3(moveInput.x * speed * Time.deltaTime, 0, 0);



    }

}
