using Unity.VisualScripting;
using UnityEngine;
public static class PlayerIdentifier
{
    const string PLAYER_TAG = "Player";

    public static bool IsPlayer(Collider collider)
    {
        return collider.transform.parent.gameObject.CompareTag(PLAYER_TAG);
    }

    public static bool IsPlayer(GameObject gameObject)
    {
        return gameObject.CompareTag(PLAYER_TAG);
    }

    public static SphereCartController GetKart(Collider collider)
    {
        if (IsPlayer(collider))
        {
            GameObject parent = collider.gameObject.transform.parent.gameObject;
            return parent.GetComponentInChildren<SphereCartController>();
        }

        return null;
    }

    public static SphereCartController GetKart(GameObject obj)
    {
        if (IsPlayer(obj))
        {
            GameObject cart = obj.gameObject.transform.GetChild(0).gameObject;
            return cart.GetComponent<SphereCartController>();
        }

        return null;
    }

    public static GameObject GetKartObject(GameObject parent)
    {
        if (IsPlayer(parent))
        {
            GameObject obj = parent.gameObject.transform.GetChild(0).gameObject;
            return obj;
        }

        return null;
    }

    public static GameObject GetPlayerGameObject(Collider collider)
    {
        if (IsPlayer(collider))
        {
            GameObject parent = collider.gameObject.transform.parent.gameObject;
            return parent;
        }

        return null;
    }

    public static CartGameSettings GetCartGameSettings(Collider collider)
    {
        if (IsPlayer(collider))
        {
            GameObject parent = collider.gameObject.transform.parent.gameObject;
            return parent.GetComponent<CartGameSettings>();
        }

        return null;
    }

}
