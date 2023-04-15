using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject TriggerElement;
    public float Velocity;
    // public LayerMask BounceMask;
    public LayerMask TargetMask;

    // Effects
    public float VelocityToAdd;
    public bool IsFreezingFunctions;
    public float TargetForce;
    public Vector3 TargetForceDirection;
    public float LifeTime;
    public float StunTime;

    private Rigidbody _rb;
    private GameObject _gb;
    private Vector3 _direction;
    private float _timer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _timer = LifeTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((TargetMask & (1 << other.gameObject.layer)) != 0)
        {
            _gb = other.gameObject;
            RunEffect();
        }
    }

    void Update()
    {
        if (_timer >= 0)
        {
            OnGoingUntilHit();
            _timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnGoingUntilHit()
    {
        _direction = TriggerElement.transform.forward;
        _rb.AddForce(_direction * Velocity, ForceMode.Acceleration);
    }

    private void RunEffect()
    {
        
        if (AIIdentifier.IsAI(_gb))
        {
            RunOnAI();
        }

        if(PlayerIdentifier.IsPlayer(_gb))
        {
            RunOnPlayer();
        }
    }

    public void RunOnPlayer()
    {
        SphereCartController _goKart =  PlayerIdentifier.GetKart(_gb);

        if (IsFreezingFunctions)
        {
            _goKart.OnStun(StunTime);
        }
        if (VelocityToAdd != 0)
        {
            _goKart.OnBoost(VelocityToAdd, StunTime);
        }
        if (TargetForce > 0)
        {
            _goKart.AddForce(TargetForce, TargetForceDirection);
        }

        Destroy(gameObject, 2);
        return;
    }

    public void RunOnAI()
    {
        AICartController  _goKart = AIIdentifier.GetAIKart(_gb);

        if (IsFreezingFunctions)
        {
            _goKart.OnStun(StunTime);
        }
        if (VelocityToAdd != 0)
        {
            _goKart.OnBoost(VelocityToAdd, StunTime);
        }
        if (TargetForce > 0)
        {
            _goKart.AddForce(TargetForce, TargetForceDirection);
        }

        Destroy(gameObject, 2);
        return;
    }
}
