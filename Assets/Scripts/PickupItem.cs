using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] int amount;

    private void OnTriggerEnter(Collider other)
    {
        Actor obj = other.gameObject.GetComponentInParent<Actor>();
        Debug.Log(other.gameObject.name);
        if(obj != null)
        {
            obj.TakeDamage(amount);
            Destroy(gameObject);
        }
    }

}
