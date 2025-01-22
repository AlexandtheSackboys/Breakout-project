using UnityEngine;

public class Perspective_Change : MonoBehaviour
{
    private bool Camera_position;
    public GameObject Camera;
    public GameObject Perspective_3rd;
    public GameObject Perspective_OG;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera.transform.position = Perspective_3rd.transform.position;
        Camera.transform.rotation = Perspective_3rd.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (Camera_position == true))
        {

            Camera.transform.position = Perspective_3rd.transform.position;

            Camera.transform.rotation = Perspective_3rd.transform.rotation;
            Camera_position = false;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && (Camera_position == false))
        {
            Camera.transform.position = Perspective_OG.transform.position;

            Camera.transform.rotation = Perspective_OG.transform.rotation;
            Camera_position = true;
        }


    }
}

