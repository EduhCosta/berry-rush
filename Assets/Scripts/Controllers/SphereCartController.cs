using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SphereCartController : MonoBehaviour
{
    [SerializeField] public Rigidbody SphereCollider;
    
    [Header("Cart properties")]
    [SerializeField] public float Acceleration = 30f;
    [SerializeField] public float Steering = 30f;
    [SerializeField] public float Gravity = 10f;

    [SerializeField] public float BoostAcceleration = 60f;
    [SerializeField] public float CurrentSpeed;

    private float _accelerateButtonValue;
    private float _steeringButtonValue;

    // Local variables
    private float _speed;
    private float _currentSpeed;
    private float _rotation;
    private float _currentRotation;
    private bool _isBreaking = false;
    private bool _isDrifting = false;
    private int _driftingDirection;
    private float _driftPower;

    // Loop variables
    private float _timeToAddForce;
    private float _newSpeed;
    private float _timeKeepingAcceleration;
    private float _timeBoost;

    private void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);

    }

    public void OnEnable()
    {
        PlayerInputControllerActions.Accelerate += OnAccelerate;
        PlayerInputControllerActions.Steering += OnSteering;
    }

    public void OnDisable()
    {
        PlayerInputControllerActions.Accelerate -= OnAccelerate;
        PlayerInputControllerActions.Steering -= OnSteering;
    }

    void Update()
    {
        // Follow sphere
        transform.position = SphereCollider.transform.position - new Vector3(0, 0.4f, 0);
        // Input Accelerate
        if (_timeKeepingAcceleration <= 0)
        {
            _speed = _accelerateButtonValue * Acceleration;
        }
        else
        {
            _speed = _accelerateButtonValue * _timeBoost;
            _timeKeepingAcceleration -= Time.deltaTime;
        }
        // Input Steer
        _rotation = _steeringButtonValue * Steering;
        // Input Break
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isBreaking = true;
        }
        else
        {
            _isBreaking = false;
        }

        Drift();

        // Accelerate
        _timeToAddForce = _isBreaking ? 3f : 12f; // Time to break : Time to max acceletate
        _newSpeed = _isBreaking ? 0 : _speed;
        _currentSpeed = Mathf.SmoothStep(_currentSpeed, _newSpeed, Time.deltaTime * _timeToAddForce);
        // Steer
        _currentRotation = Mathf.Lerp(_currentRotation, _rotation, Time.deltaTime * 4f);


        CurrentSpeed = _currentSpeed;
    }

    void FixedUpdate()
    {
        // Run
        SphereCollider.AddForce(transform.forward * _currentSpeed, ForceMode.Acceleration);

        // Gravity
        SphereCollider.AddForce(Vector3.down * Gravity, ForceMode.Acceleration);

        // Steering
        transform.eulerAngles = Vector3.Lerp(
            transform.eulerAngles,
            new Vector3(0, transform.eulerAngles.y + _currentRotation, 0),
            Time.deltaTime * 5f
        );
    }

    private void Drift()
    {
        if (Input.GetButtonDown("Jump") && !_isDrifting && Input.GetAxis("Horizontal") != 0)
        {
            Debug.Log("DIFTING");
            _driftPower = 0;
            _isDrifting = true;
            _driftingDirection = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
        }

        if (_isDrifting)
        {
            float control = (_driftingDirection == 1) ? ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 0, 2) : ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 2, 0);
            float powerControl = (_driftingDirection == 1) ? ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, .2f, 1) : ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 1, .2f);
            _rotation = Steering * _driftingDirection * control; 
            _driftPower += powerControl;
        }

        if (Input.GetButtonUp("Jump") && _isDrifting)
        {
            // Boost 
            Debug.Log($"BOOST {_driftPower}");
            OnBoost(_driftPower); // Setting to control the boost
            _isDrifting = false;
        }
    }

    public void OnBoost(float boostPower)
    {
        _currentSpeed = boostPower + Acceleration;
    }

    public void OnBoost(float boostPower, float time)
    {
        _timeBoost = boostPower + Acceleration;
        _timeKeepingAcceleration = time;
    }

    public void OnStun(float time)
    {
        _timeBoost = 0;
        _timeKeepingAcceleration = time;
    }

    private void OnAccelerate(float value)
    {
        _accelerateButtonValue = value;
        
    }
    private void OnSteering(float value)
    {
        _steeringButtonValue = value;
    }
}
