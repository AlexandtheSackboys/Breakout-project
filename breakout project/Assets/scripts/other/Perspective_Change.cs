using UnityEngine;

public class Perspective_Change : MonoBehaviour
{
    private bool camera_Position;
    public GameObject CameraObject;
    public GameObject perspective_3rd;
    public GameObject perspective_OG;
    public Paddle_Controller fixedCamera_Aim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CameraObject.transform.position = perspective_3rd.transform.position;
        CameraObject.transform.rotation = perspective_3rd.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        while (fixedCamera_Aim.isAimActive == false) 
            /* this while loop above will make sure that the camera remains fixed
             while the aim power up is active*/
        {
            if (Input.GetKeyDown(KeyCode.Space) && (camera_Position == true))
            {

                CameraObject.transform.position = perspective_3rd.transform.position;

                CameraObject.transform.rotation = perspective_3rd.transform.rotation;
                camera_Position = false;

            }
            else if (Input.GetKeyDown(KeyCode.Space) && (camera_Position == false))
            {
                CameraObject.transform.position = perspective_OG.transform.position;

                CameraObject.transform.rotation = perspective_OG.transform.rotation;
                camera_Position = true;
            }
            return;
        }


    }
}

