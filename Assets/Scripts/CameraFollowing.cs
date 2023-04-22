using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] private Vector3 OffsetCam = new Vector3(0, 3, 5);

    // local variables
    Vector3 _tmpPosition;
    private Transform _cam;

    void Start()
    {
      _cam = GetComponent<Transform>();   
    }

    void FixedUpdate()
    {
        SetCameraPostion();
        SetCameraDirection();
    }

    private void SetCameraPostion()
    {
        _tmpPosition = Target.position;
        _tmpPosition -= Target.forward * OffsetCam.z;
        _tmpPosition += Target.right * OffsetCam.x;
        _tmpPosition += Target.up * OffsetCam.y;

        _cam.position = Vector3.Lerp(_cam.position, _tmpPosition, Time.deltaTime * 30f);
    }

    private void SetCameraDirection()
    {
        _cam.LookAt(Target, Target.up);
    }
}
