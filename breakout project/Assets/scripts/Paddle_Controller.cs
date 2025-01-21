using UnityEngine;

public class Paddle_Controller : MonoBehaviour
{
    public float Speed = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // references a list of inputs within the project settings  in the unity editor
        float MovementInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * Speed * MovementInput);
    }
}
