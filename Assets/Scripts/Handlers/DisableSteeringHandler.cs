using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSteeringHandler : MonoBehaviour
{
    public static Action<bool> DisableSteering;

    [SerializeField] public LayerMask GroundLayerMask;
    [SerializeField] public float _cartWidth = 1.4f;

    private RaycastHit hit;
    private bool _previousIsGround;
    private bool _isOnGround;

    void FixedUpdate()
    {
        Checking();
        CallAction();
    }

    private void Checking()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * _cartWidth, out hit, _cartWidth, GroundLayerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            _isOnGround = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _cartWidth, Color.magenta);
            _isOnGround = false;
        }
    }

    private void CallAction()
    {
        if (_previousIsGround != _isOnGround)
        {
            if (_isOnGround)
            {
                DisableSteering(false);
            }
            else
            {
                DisableSteering(true);
            }
            _previousIsGround = _isOnGround;
        }
    }

}
