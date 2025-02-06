using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle_Controller : MonoBehaviour
{
    [SerializeField][Range(5000,30000)] private float speed;
    [Range(20,30)] public float ball_releaseSpeed;
    [Range(3, 10)] public float timeTilRelease;
    
    private new Rigidbody rigidbody;
    private Vector2 moveInput;
    private Animator tilt;


    public GameObject ballPrefab;
    public Rigidbody ballRb;
    public GameObject recallPoint;


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
            StartCoroutine(Aim_time());

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

    IEnumerator Aim_time()
    {
        // recallpoint will be the ballSpawner child GameObject
        ballPrefab.transform.position = recallPoint.transform.position;

        ballPrefab.transform.SetParent(recallPoint.transform,true); // makes the gameobject a child of the refrenced game object
        
        ballRb.constraints = RigidbodyConstraints.FreezePositionX 
            | RigidbodyConstraints.FreezePositionY 
            | RigidbodyConstraints.FreezePositionZ; //causes the rigidbodys position in all 3 directions to freeze

        yield return new WaitForSecondsRealtime(timeTilRelease);

        Debug.Log("Recall");

        ballPrefab.transform.SetParent(null); // sperate child and parent game object

        ballRb.constraints = RigidbodyConstraints.FreezePositionY; //causes the rigidbodys position in the y directions to freeze

        ballRb.linearVelocity = new Vector3(5, 0, ball_releaseSpeed);
        StopCoroutine(Aim_time());


    }
       


}
