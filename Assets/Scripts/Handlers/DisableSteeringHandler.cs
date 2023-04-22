using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSteeringHandler : MonoBehaviour
{
    [SerializeField] public LayerMask GroundLayerMask;
    [SerializeField] public float CartWidth = 1.4f;

    private RaycastHit hit;
    private bool _previousIsGround;
    private bool _isOnGround;
    private IKartController controller;

    private void Start()
    {
        controller = gameObject.transform.parent.GetComponentInChildren<IKartController>();
    }

    void FixedUpdate()
    {
        Checking();
        CallAction();
    }

    private void Checking()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * CartWidth, out hit, CartWidth, GroundLayerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            _isOnGround = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * CartWidth, Color.magenta);
            _isOnGround = false;
        }
    }

    private void CallAction()
    {
        if (_previousIsGround != _isOnGround)
        {
            if (_isOnGround)
            {
                controller.OnDisableSteering(false);
            }
            else
            {
                controller.OnDisableSteering(true);
            }
            _previousIsGround = _isOnGround;
        }
    }

}
