using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle_Controller : MonoBehaviour
{
    [Range(5000,30000)] public float speed;
    private new Rigidbody rigidbody;
    private Vector2 moveInput;
    private Animator tilt;
    [SerializeField] private AudioSource hit;
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Call");
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
    }

}
