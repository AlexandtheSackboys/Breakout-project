using UnityEngine;

public class PerspectiveChange : MonoBehaviour
{
    private bool _cameraPosition;
    public GameObject CameraObject, PerspectiveThird;

    [SerializeField] private GameObject _perspectiveClassic;
    // Classic refers to the original camera perspective in classic Arkanoid/Breakout
    
    private PaddleController _paddleController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CameraObject.transform.position = PerspectiveThird.transform.position;
        CameraObject.transform.rotation = PerspectiveThird.transform.rotation;
        _paddleController = GameObject.Find("Player_Paddle").GetComponent<PaddleController>();

    }

    // Update is called once per frame
    void Update()
    {
        while (_paddleController.AimActive == false) 
            /* this while loop above will make sure that the camera remains fixed
             while the aim power up is active*/
        {
            if (Input.GetKeyDown(KeyCode.Space) && (_cameraPosition == true))
            {

                CameraObject.transform.position = PerspectiveThird.transform.position;

                CameraObject.transform.rotation = PerspectiveThird.transform.rotation;
                _cameraPosition = false;

            }
            else if (Input.GetKeyDown(KeyCode.Space) && (_cameraPosition == false))
            {
                CameraObject.transform.position = _perspectiveClassic.transform.position;

                CameraObject.transform.rotation = _perspectiveClassic.transform.rotation;
                _cameraPosition = true;
            }
            return;
        }


    }
}

