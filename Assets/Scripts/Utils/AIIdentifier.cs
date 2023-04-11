using UnityEngine;
public static class AIIdentifier
{
    const string AI_TAG = "AI";

    public static bool IsAI(Collider collider)
    {
        return collider.transform.parent.gameObject.CompareTag(AI_TAG);
    }

    public static bool IsAI(GameObject gameObject)
    {
        return gameObject.CompareTag(AI_TAG);
    }

    public static AICartController GetAIKart(Collider collider)
    {
        if (IsAI(collider))
        {
            GameObject parent = collider.gameObject.transform.parent.gameObject;
            return parent.GetComponentInChildren<AICartController>();
        }

        return null;
    }

    public static string GetAIId(GameObject gameObject)
    {
        return gameObject.GetComponentInParent<CartGameSettings>().GetPlayerId();
    }

    public static string GetName(GameObject gameObject)
    {
        return gameObject.GetComponentInParent<CartGameSettings>().PlayerName;
    }

    public static GameObject GetAIGameObject(Collider collider)
    {
        if (IsAI(collider))
        {
            GameObject parent = collider.gameObject.transform.parent.gameObject;
            return parent;
        }

        return null;
    }

    public static CartGameSettings GetCartGameSettings(Collider collider)
    {
        if (IsAI(collider))
        {
            GameObject parent = collider.gameObject.transform.parent.gameObject;
            return parent.GetComponent<CartGameSettings>();
        }

        return null;
    }
}
