using FMODUnity;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    // stats that can be edited in editor
    [SerializeField][Range(5000, 30000)] private float _paddleSpeed;
    [SerializeField][Range(20, 30)] private float _ballReleaseSpeed;

    [SerializeField][Range(3, 10)] private float _timeRelease, _timeTilShrink;
    [SerializeField][Range(1,5)]  private float _spreadLifetime;

    [SerializeField][Range(0.5f, 2.5f)] private float _paddleExtension;
    [SerializeField][Range(2, 8)] private float _slowDown;

    // effected by player input
    private Rigidbody _paddleRigidbody;
    private Vector2 _moveInput;
    private Animator _paddleTilt;

    // player object references
    [SerializeField] private GameObject _perspectivePowerUp;

    private PerspectiveChange _cameraChange;

    
    [SerializeField]private GameObject _ballPrefab;
    [SerializeField]private Rigidbody _ballRb;

    [SerializeField] private GameObject _recallPoint, _leftPaddleEnd ,_rightPaddleEnd;


    // power up activation 
    [HideInInspector] public bool SpreadActivate = false, AimActive = false, GatherLife = false, ScaleActive = false;

    // other variables
    [SerializeField] private FakerScript _temporaryBalls;
    [SerializeField] private StudioEventEmitter _paddleHit;
    private PauseMenu _paused;

    // Power Up texxt objects
    [SerializeField] private GameObject _aimText,_extendText, _spreadText;


    // Timers
    private float _aimTimer = 0f, _scaleTimer = 0f;
    [HideInInspector] public float SpreadTextTimer = 0f;
    
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cameraChange = GameObject.Find("Main Camera").GetComponent<PerspectiveChange>();

        _paused = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        _paddleRigidbody = GetComponent<Rigidbody>();
        _paddleTilt = GetComponent<Animator>();
    }

    // Update is called once per frame   
    void Update()
    {
        MovementHandler();
        HandleTimers();
    }

    // function below deals with animation and movement of paddle 
    private void MovementHandler()
    {
        if (_paddleTilt != null)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                _paddleTilt.SetTrigger("TiltR_Open");

            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                _paddleTilt.SetTrigger("TiltR_Close");

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                _paddleTilt.SetTrigger("TiltL_Open");

            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                _paddleTilt.SetTrigger("TiltL_Close");

            _paddleRigidbody.linearVelocity = new Vector3(_moveInput.x * _paddleSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void HandleTimers()
    {
        if (!_paused.IsPaused)
        {
            // Aim Timer
            if (AimActive && Time.time >= _aimTimer)
            {
                AimTime();
            }

            // Scale Timer
            if (ScaleActive && Time.time >= _scaleTimer)
            {
                ScaleUp();
            }
            if (SpreadActivate && Time.time >= SpreadTextTimer)
            {
                Spread();

            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _paddleHit.Play();


        }
    }


    

    public void AimTime()
    {
        if (!AimActive)
        {
            AimActive = true;
            _aimTimer = Time.time + _timeRelease; // will end power up when Time.time is equal to aimTimer as Time.time always counts up

            Debug.Log("Time: " + Time.time);
            Debug.Log("Aim Timer: " + _aimTimer);

            _ballPrefab.transform.position = _recallPoint.transform.position;
            _ballPrefab.transform.SetParent(_recallPoint.transform, true);
            _ballRb.constraints = RigidbodyConstraints.FreezeAll; // freezes rigidbody
            _aimText.SetActive(true);


            _cameraChange.CameraObject.transform.position = _perspectivePowerUp.transform.position;
            _cameraChange.CameraObject.transform.rotation = _perspectivePowerUp.transform.rotation;
            _cameraChange.CameraObject.transform.SetParent(_perspectivePowerUp.transform, true);
            // changes camera perspective closer to the paddle
            return;
        }
        Debug.Log("Aim Power-up Ended");

        _aimText.SetActive(false);
        _ballPrefab.transform.SetParent(null);
        _ballRb.constraints = RigidbodyConstraints.FreezePositionY;

        _cameraChange.CameraObject.transform.SetParent(null);
        _cameraChange.CameraObject.transform.position = _cameraChange.PerspectiveThird.transform.position;
        _cameraChange.CameraObject.transform.rotation = _cameraChange.PerspectiveThird.transform.rotation;

        _ballRb.linearVelocity = new Vector3(5, 0, _ballReleaseSpeed);
        AimActive = false;
        // causes padddle to return to its original state when Time.time is equal to aimTimer

    }




    // Activate Scale Power-up
    public void ScaleUp()
    {
        if (!ScaleActive)
        {
            ScaleActive = true;
            _scaleTimer = Time.time + _timeTilShrink; // will end power up when Time.time is equal to scaleTimer as Time.time always counts up
            Debug.Log("Time: " + Time.time);
            Debug.Log("Scale Timer: " + _scaleTimer);
            _extendText.SetActive(true);

            _leftPaddleEnd.transform.localScale = new Vector3(_paddleExtension, 1.46672726f, 1.54101229f);
            _rightPaddleEnd.transform.localScale = new Vector3(_paddleExtension, 1.46672726f, 1.54101229f);
            // changes the length of the edges of the paddle

            _paddleSpeed /= _slowDown;
            return;
        }

        Debug.Log("Scale Power-up Ended");
        _extendText.SetActive(false);
        _leftPaddleEnd.transform.localScale = new Vector3(0.115734726f, 1.46672726f, 1.54101229f);
        _rightPaddleEnd.transform.localScale = new Vector3(0.115734726f, 1.46672726f, 1.54101229f);

        _paddleSpeed *= _slowDown;
        ScaleActive = false;
        // causes padddle to return to its original state when Time.time is equal to scaleTimer
    }

    public void Spread()
    {

        if (!SpreadActivate)
        {
            SpreadActivate = true;
            SpreadTextTimer = Time.time + _spreadLifetime;
            _spreadText.SetActive(true);
            Rigidbody fakerRb = Instantiate(_temporaryBalls.FakerRb, new Vector3(gameObject.transform.position.x, _ballPrefab.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
            fakerRb.AddForce(_temporaryBalls.MagnitudeX, 0, _temporaryBalls.MagnitudeZ);



            Rigidbody fakerRb2 = Instantiate(_temporaryBalls.FakerRb, new Vector3(gameObject.transform.position.x, _ballPrefab.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
            fakerRb2.AddForce(-_temporaryBalls.MagnitudeX, 0, _temporaryBalls.MagnitudeZ);
            // produces 2 temporary balls which have a limited number of times that they can hit a block
            return;

        }

        _spreadText.SetActive(false);
        SpreadActivate = false;


    }
}

