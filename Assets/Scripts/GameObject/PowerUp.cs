using System;
using Unity.VisualScripting;
using UnityEditor;
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
    [Tooltip("If true the powerUp is not handled by user, this tigger right after selected")]
    [SerializeField] public bool IsHotTriggered;
    [SerializeField] private Mesh _DamageRange;

    [Header("Effects")]
    [SerializeField] private float _velocityToAdd;

    // Props
    // private LayerMask _target;
    // private float _lifeTime;
    private AnimationClip _clip;

    // Definer
    private bool _isBullet;

    // Not bullet
    // private Mesh _DamageRange;

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
        if (_DamageRange != null)
        {
            GameObject collider = GenerateCollider(cart);
            AddMeshScripts(collider);
        }
    }

    private GameObject GenerateCollider(GameObject player)
    {
        GameObject cart = PlayerIdentifier.GetKartObject(player);
        GameObject collider = new GameObject();
        collider.name = _name + "_Collider";
        collider.transform.position = cart.transform.position;
        MeshCollider meshCollider = collider.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = _DamageRange;
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
        collider.transform.parent = cart.transform;
        collider.transform.rotation = new Quaternion();

        return collider;
    }

    private void AddMeshScripts(GameObject collider)
    {
        if (_id == 3) // Pesticide
        {
            Pesticide p = collider.AddComponent<Pesticide>();
            p.timeValue = _lifeTime;
            p.target = _target;
        }
    }
}
