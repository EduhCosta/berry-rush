using System.Collections;
using UnityEngine;

public class AICartController : IKartController
{
    [SerializeField] public Rigidbody SphereCollider;

    [Header("Cart properties")]
    [SerializeField] public float Acceleration = 30f;
    [SerializeField] public float Steering = 30f;
    [SerializeField] public float Gravity = 10f;
    [SerializeField] public Color GizmoColor = Color.gray;


    // Local variables
    private float _speed;
    private float _currentSpeed;
    private float _rotation;
    private float _currentRotation;
    private bool _isBreaking = false;

    // Loop variables
    private float _timeToAddForce;
    private float _newSpeed;
    private float _timeKeepingAcceleration;
    private float _timeBoost;
    private bool _isFreezing;
    private AIInputController _controller;
    [SerializeField]  private float _driftPower = 0f;
    private float _driftingDirection = 0f;

    private void OnDrawGizmos()
    {
        // Draws a 5 unit long line in front of the object
        Gizmos.color = GizmoColor;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);

    }

    private void Start()
    {
        _controller = GetComponent<AIInputController>();
    }

    void Update()
    {
        // Follow sphere
        transform.position = SphereCollider.transform.position - new Vector3(0, 0.4f, 0);
        // Input Accelerate
        float accelerate = _controller.Accelerate;
        if (_timeKeepingAcceleration <= 0)
        {
            _speed = accelerate * Acceleration;
        }
        else
        {
            _speed = accelerate * _timeBoost;
            _timeKeepingAcceleration -= Time.deltaTime;
        }
        // Input Steer
        float direction = _controller.Direction;
        _rotation = direction * Steering;

        // Drift
        Drift();

        // Input Break
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isBreaking = true;
        }
        else
        {
            _isBreaking = false;
        }

        // Accelerate
        _timeToAddForce = _isBreaking ? 3f : 12f; // Time to break : Time to max acceletate
        _newSpeed = _isBreaking ? 0 : _speed;
        _currentSpeed = Mathf.SmoothStep(_currentSpeed, _newSpeed, Time.deltaTime * _timeToAddForce);
        // Steer
        _currentRotation = Mathf.Lerp(_currentRotation, _rotation, Time.deltaTime * 4f);
    }

    void FixedUpdate()
    {
        if (!_isFreezing)
        {
            // Run
            SphereCollider.AddForce(transform.forward * _currentSpeed, ForceMode.Acceleration);

            //Gravity
            SphereCollider.AddForce(Vector3.down * Gravity, ForceMode.Acceleration);

            if (_isDisableSteering == false)
            {
                // Steering
                transform.eulerAngles = Vector3.Lerp(
                    transform.eulerAngles,
                    new Vector3(0, transform.eulerAngles.y + _currentRotation, 0),
                    Time.deltaTime * 5f
                );
            }
        }
    }

    private void Drift()
    {
        float direction = _controller.Direction;
        _driftingDirection = direction > 0 ? 1 : -1;

        if (_controller.IsDrifting && direction != 0)
        {
            float control = (_driftingDirection == 1) ? ExtensionMethods.Remap(direction, -1, 1, 0, 2) : ExtensionMethods.Remap(direction, -1, 1, 2, 0);
            float powerControl = (_driftingDirection == 1) ? ExtensionMethods.Remap(direction, -1, 1, .2f, 1) : ExtensionMethods.Remap(direction, -1, 1, 1, .2f);
            _rotation = 20f * _driftingDirection * control;
            _driftPower += powerControl;
        }

        if (!_controller.IsDrifting && _driftPower > 0)
        {
            OnBoost(_driftPower);
            _driftPower = 0;
            _driftingDirection = _controller.Direction > 0 ? 1 : -1;
        }
    }

    public void OnBoost(float boostPower)
    {
        if (boostPower < Acceleration)
            _currentSpeed = boostPower + Acceleration;
        else
            _currentSpeed = 2 * Acceleration;
    }

    public void OnBoost(float boostPower, float time)
    {
        _timeBoost = boostPower + Acceleration;
        _timeKeepingAcceleration = time;
    }

    public void OnStun(float timeToKeepFreezing)
    {
        _isFreezing = true;
        SphereCollider.velocity = Vector3.zero;
        SphereCollider.angularVelocity = Vector3.zero;
        StartCoroutine(ResetFreezing(timeToKeepFreezing));
    }

    IEnumerator ResetFreezing(float timeToKeepFreezing)
    {
        yield return new WaitForSeconds(timeToKeepFreezing); // Time to keep stuning
        _isFreezing = false;
    }

    public void AddForce(float force, Vector3 direction)
    {
        SphereCollider.AddForce(force * Time.deltaTime * direction);
    }
}
