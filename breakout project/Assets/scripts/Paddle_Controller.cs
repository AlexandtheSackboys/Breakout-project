using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle_Controller : MonoBehaviour
{
    [Range(5000,20000)] public float speed;
    private new Rigidbody rigidbody;
    private Vector2 moveInput;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Call");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // references a list of inputs within the project settings  in the unity editor
        //float MovementInput = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.right * Time.deltaTime * Speed * MovementInput);
        rigidbody.linearVelocity = new Vector3(moveInput.x*speed*Time.deltaTime,0,0);
    }
}
