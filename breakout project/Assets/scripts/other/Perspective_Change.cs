using UnityEngine;

public class Perspective_Change : MonoBehaviour
{
    private bool camera_Position;
    public GameObject CameraObject;
    public GameObject Perspective_3rd;
    public GameObject Perspective_OG;
    private PaddleController paddle_Controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CameraObject.transform.position = Perspective_3rd.transform.position;
        CameraObject.transform.rotation = Perspective_3rd.transform.rotation;
        paddle_Controller = GameObject.Find("Player_Paddle").GetComponent<PaddleController>();

    }

    // Update is called once per frame
    void Update()
    {
        while (paddle_Controller.AimActive == false) 
            /* this while loop above will make sure that the camera remains fixed
             while the aim power up is active*/
        {
            if (Input.GetKeyDown(KeyCode.Space) && (camera_Position == true))
            {

                CameraObject.transform.position = Perspective_3rd.transform.position;

                CameraObject.transform.rotation = Perspective_3rd.transform.rotation;
                camera_Position = false;

            }
            else if (Input.GetKeyDown(KeyCode.Space) && (camera_Position == false))
            {
                CameraObject.transform.position = Perspective_OG.transform.position;

                CameraObject.transform.rotation = Perspective_OG.transform.rotation;
                camera_Position = true;
            }
            return;
        }


    }
}

