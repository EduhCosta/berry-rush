using System;
using UnityEngine;

public class PowerUp: MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Texture _thumbnail;

    // Props
    private LayerMask _target;
    private float _lifeTime;
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
    private float _velocityToadd;
    private bool _isFreezingFunctions;
    private bool _isInvencible;
    private float _targeForce;

    public Texture GetThumbnail()
    {
        return _thumbnail;
    }

    public int GetId()
    {
        return _id;
    }
}
