using System;
using UnityEngine;

public class PowerUp: MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Texture _thumbnail;

    [Header("Properties")]
    [SerializeField] private LayerMask _target;
    [Tooltip("Time to end the effects of powerup in seconds")]
    [SerializeField] private float _lifeTime;

    [Header("Effects")]
    [SerializeField] private float _velocityToAdd;

    // Props
    // private LayerMask _target;
    // private float _lifeTime;
    private AnimationClip _clip;

    // Definer
    private bool _isBullet;

    // Not bullet
    private Mesh _DamageRange;

    // IsBullet
    private float _bulletVelocity;
    private LayerMask _maskToBounce;
    private int _bouncesQTDBeforeDestroy;

    // Effects
    // private float _velocityToadd;
    private bool _isFreezingFunctions;
    private bool _isInvencible;
    private float _targeForce;

    private void Awake()
    {
        // if (_lifeTime <= 0) throw new Exception("Lifetime is a required prop to PowerUps");
    }

    public Texture GetThumbnail()
    {
        return _thumbnail;
    }

    public int GetId()
    {
        return _id;
    }

    public void Run(GameObject cart)
    {
        SphereCartController cartController = cart.GetComponentInChildren<SphereCartController>();
        if (_velocityToAdd != 0) cartController.OnBoost(_velocityToAdd, _lifeTime);
    }
}
