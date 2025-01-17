using UnityEngine;

public class Paddle_Controller : MonoBehaviour
{
    public float Speed = 10;
    private float MovementInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * Speed * MovementInput);
    }
}
