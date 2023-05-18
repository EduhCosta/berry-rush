using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandleFieldOfView : MonoBehaviour
{
    [SerializeField] private int LowFieldOfView = 60;
    [SerializeField] private int HighFieldOfView = 80;
    [SerializeField] private float Step = 3f;
    [SerializeField] private SphereCartController SphereCartController;

    private int _defaultFieldOfView = 70;
    private float _currentSpeed;
    private Camera _cam;

    private void Start()
    {
        _cam = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        _currentSpeed = SphereCartController.CurrentSpeed;
        if (_currentSpeed < 50) _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, LowFieldOfView, Step * Time.deltaTime);
        if (_currentSpeed > 50 && _currentSpeed < 105) _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _defaultFieldOfView, Step * Time.deltaTime);
        if (_currentSpeed > 105) _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, HighFieldOfView, Step * Time.deltaTime);
    }
}
