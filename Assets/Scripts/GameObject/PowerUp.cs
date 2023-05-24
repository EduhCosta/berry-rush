using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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
    // [SerializeField] private float _bulletLifeTime;
    [SerializeField] private Mesh _bulletAspect;
    [SerializeField] private Material _bulletMaterial;
    [SerializeField] private int _bulletTargetByRaceRaking = 0;
    //[Tooltip("Deinfe the position of power up bullet will go")]
    //[SerializeField] private int _targetPosition;


    [Header("Effects")]
    [SerializeField] private float _velocityToAdd;
    [SerializeField] private bool _isFreezingFunctions;
    [SerializeField] private bool _isInvencible;
    [SerializeField] private float _targeForce;

    [Header("Feedbacks")]
    [SerializeField] public AudioClip FeedbackAudio;
    [SerializeField] public ParticleSystem FeedbackParticles;

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

    public float GetLifeTime()
    {
        return _lifeTime;
    }

    public void Run(GameObject cart)
    {
        SphereCartController cartController = cart.GetComponentInChildren<SphereCartController>();
        if (_velocityToAdd != 0) cartController.OnBoost(_velocityToAdd, _lifeTime);

        if (_isBullet)
        {
            GenerateBullet(cart);
        }
    }

    private GameObject GenerateBullet(GameObject player)
    {
        GameObject cart = PlayerIdentifier.GetKartObject(player);
        GameObject bullet = new GameObject();

        bullet.name = _name + "_Bullet";
        bullet.transform.position = cart.transform.position + (cart.transform.forward * 10);
        bullet.transform.forward = cart.transform.forward;

        MeshFilter mf = bullet.AddComponent<MeshFilter>();
        mf.mesh = _bulletAspect;

        bullet.AddComponent<MeshRenderer>();
        bullet.GetComponent<Renderer>().material = _bulletMaterial;

        BoxCollider bc = bullet.AddComponent<BoxCollider>();
        bc.isTrigger = true;

        NavMeshAgent nma = bullet.AddComponent<NavMeshAgent>();
        nma.speed = _bulletSpeed;
        nma.acceleration = _bulletSpeed;

        BulletPowerUp bpu = bullet.AddComponent<BulletPowerUp>();
        if (_bulletTargetByRaceRaking > 0) 
        {
            SettingPowerUpBullerProps(bpu, _bulletTargetByRaceRaking);
        } 
        else
        {
            string id = player.GetComponent<CartGameSettings>().GetPlayerId();
            SettingPowerUpBullerProps(bpu, PodiumStore.Instance.GetCurrentPosition(id) - 1);
        }

        return bullet;
    }

    private void SettingPowerUpBullerProps(BulletPowerUp bpu, int position)
    {
        GameObject target = PodiumStore.Instance.GetPlayerByPosition(position);
        IKartController kartCtrl = target.GetComponentInChildren<IKartController>();
        bpu.SetTarget(kartCtrl.gameObject);
        bpu.StunTime = _lifeTime;
        CartGameSettings settings = target.GetComponent<CartGameSettings>();
        bpu.TargetId = settings.GetPlayerId();
    }
}
