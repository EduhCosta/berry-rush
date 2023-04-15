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
    // [SerializeField] private Mesh _DamageRange;
    [Tooltip("Define if this PowerUp is a bullet")]

    [Header("Bullet Properties")]
    [SerializeField] private bool _isBullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLifeTime;
    [SerializeField] private Mesh _bulletAspect;
    [SerializeField] private Material _bulletMaterial;
    //[Tooltip("Deinfe the position of power up bullet will go")]
    //[SerializeField] private int _targetPosition;


    [Header("Effects")]
    [SerializeField] private float _velocityToAdd;
    [SerializeField] private bool _isFreezingFunctions;
    [SerializeField] private bool _isInvencible;
    [SerializeField] private float _targeForce;

    // Props
    // private LayerMask _target;
    // private float _lifeTime;
    private AnimationClip _clip;

    // Definer
    // private bool _isBullet;

    // Not bullet
    // private Mesh _DamageRange;

    // IsBullet
    // private float _bulletVelocity;
    private LayerMask _maskToBounce;
    private int _bouncesQTDBeforeDestroy;

    // Effects
    // private float _velocityToadd;
    //private bool _isFreezingFunctions;
    //private bool _isInvencible;
    //private float _targeForce;


    private GameObject _triggerElement;
    private GameObject _targetElement;



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

        if (_isBullet)
        {
            GenerateBullet(cart);


            //if (_targetPosition != 0)
            //{
            //    GameObject targetElement = PodiumStore.Instance.GetPlayerByPosition(_targetPosition);
            //    // Identificar se a distância entre o próximo checkpoint ou o alvo é a mais próxima, a que for o projétil vai seguir
            //}

        }
    }

    private GameObject GenerateCollider(GameObject player)
    {
        GameObject cart = PlayerIdentifier.GetKartObject(player);
        GameObject collider = new GameObject();
        collider.name = _name + "_Collider";
        collider.transform.position = cart.transform.position;
        MeshCollider meshCollider = collider.AddComponent<MeshCollider>();
        // meshCollider.sharedMesh = _DamageRange;
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
        collider.transform.parent = cart.transform;
        collider.transform.rotation = new Quaternion();

        return collider;
    }

    private GameObject GenerateBullet(GameObject player)
    {
        GameObject cart = PlayerIdentifier.GetKartObject(player);
        GameObject bullet = new GameObject();

        bullet.name = _name + "_Bullet";
        bullet.transform.position = cart.transform.position;
        bullet.transform.forward = cart.transform.forward;

        Rigidbody rb = bullet.AddComponent<Rigidbody>();
        rb.useGravity = false;

        MeshFilter mf = bullet.AddComponent<MeshFilter>();
        mf.mesh = _bulletAspect;

        MeshRenderer mr = bullet.AddComponent<MeshRenderer>();
        mr.materials[0] = _bulletMaterial;

        BoxCollider bc = bullet.AddComponent<BoxCollider>();
        bc.isTrigger = true;

        Bullet bl = bullet.AddComponent<Bullet>();
        bl.TriggerElement = cart;
        bl.Velocity = _bulletSpeed;
        bl.TargetMask = _target;
        bl.VelocityToAdd = _velocityToAdd;
        bl.IsFreezingFunctions = _isFreezingFunctions;
        bl.TargetForce = _targeForce;
        bl.LifeTime = _bulletLifeTime;
        bl.StunTime = _lifeTime;

        return bullet;
    }

    public void OnRunEffect()
    {

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
