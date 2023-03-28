using UnityEngine;
using System;

public class CartGameSettings : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public string PlayerName;
    [SerializeField] private string _playerId;
    [SerializeField] private float _cartWidth;


    private void Awake()
    {
        TagValidationException();
        SetId();
        SetCartWidth();
    }

    /// <summary>
    /// Get width of Cart and save to local variable
    /// </summary>
    private void SetCartWidth()
    {
        _cartWidth = GetComponentInChildren<SphereCollider>().radius * 2;
    }

    /// <summary>
    /// Returns width of Cart
    /// </summary>
    public float GetCartWidth()
    {
        return _cartWidth;
    }

    /// <summary>
    /// Identify and notify the user if the Tag is correctly setted on same GameObjet with this component is attached;
    /// </summary>
    private void TagValidationException()
    {
        if (!PlayerIdentifier.IsPlayer(gameObject) && !AIIdentifier.IsAI(gameObject))
        {
            throw new Exception("Please, set this GameObject tag as `Player` or `AI`");
        }
    }

    /// <summary>
    /// Create a unique id to Cart
    /// </summary>
    private void SetId()
    {
        _playerId = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Returns the player id
    /// </summary>
    public string GetPlayerId()
    {
        return _playerId;
    }
}