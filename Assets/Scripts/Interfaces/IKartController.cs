using UnityEngine;

public abstract class IKartController: MonoBehaviour
{
    protected bool _isDisableSteering = false;

    public void OnDisableSteering(bool obj)
    {
        _isDisableSteering = obj;
    }
}

