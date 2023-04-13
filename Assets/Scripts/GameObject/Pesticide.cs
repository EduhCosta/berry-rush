using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pesticide : MonoBehaviour
{
    public float timeValue;
    public LayerMask target;

    private void Start()
    {
        StartCoroutine(DestroyByTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == target)
        {
            if (PlayerIdentifier.IsPlayer(other))
            {
                SphereCartController cart = GetComponent<SphereCartController>();
                cart.OnStun(timeValue);
            }

            if (AIIdentifier.IsAI(other))
            {
                AICartController cart = GetComponent<AICartController>();
                cart.OnStun(timeValue);
            }
        }
    }
    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(timeValue);

        Destroy(gameObject);
    }


}
