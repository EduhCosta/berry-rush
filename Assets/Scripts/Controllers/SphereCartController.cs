using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCartController : MonoBehaviour
{
    [SerializeField] public Rigidbody SphereCollider;

    [Header("Cart properties")]
    [SerializeField] public float Acceleration = 30f;
    [SerializeField] public float Steering = 30f;
    [SerializeField] public float Gravity = 10f;

    [SerializeField] public float BoostAcceleration = 60f;
    [SerializeField] public float CurrentSpeed;


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

    private void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);

    }

    void Update()
    {
        // Follow sphere
        transform.position = SphereCollider.transform.position - new Vector3(0, 0.4f, 0);
        // Input Accelerate
        _speed = Input.GetAxis("Vertical") * Acceleration;
        // Input Steer
        _rotation = Input.GetAxis("Horizontal") * Steering;
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

        //Gravity
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
            _currentSpeed = _driftPower + Acceleration; // Setting to control the boost
            _isDrifting = false;
        }
    }

}
