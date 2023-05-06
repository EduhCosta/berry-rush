using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletPowerUp : MonoBehaviour
{
    public float StunTime;
    public string TargetId;
    private NavMeshAgent _agent;
    private bool _hitDestination = false;
    private GameObject _targetGameObject = null;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (
            _targetGameObject != null && 
            _agent != null &&
            _hitDestination == false
            )
        {
            _agent.SetDestination(_targetGameObject.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHitTarget(other);
    }

    private void OnHitTarget(Collider obj)
    {
        if (PlayerIdentifier.IsPlayer(obj) && PlayerIdentifier.GetCartGameSettings(obj)?.GetPlayerId() == TargetId)
        {
            SphereCartController cart = PlayerIdentifier.GetKart(obj);
            cart.OnStun(StunTime);
            Destroy(gameObject, StunTime / 2);
        }
        if (AIIdentifier.IsAI(obj) && AIIdentifier.GetCartGameSettings(obj).GetPlayerId() == TargetId)
        {
            AICartController kart = AIIdentifier.GetAIKart(obj);
            kart.OnStun(StunTime);
            Destroy(gameObject, StunTime / 2);
        }
    }

    public void SetTarget(GameObject obj)
    {
        _targetGameObject = obj;
    }

    public void HitDestination()
    {
        _hitDestination = true;
    }
}
