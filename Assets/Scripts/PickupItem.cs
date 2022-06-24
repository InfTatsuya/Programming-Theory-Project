using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] int amount;

    private void OnTriggerEnter(Collider other)
    {
        Actor obj = other.gameObject.GetComponent<Actor>();
        if(obj != null)
        {
            obj.TakeDamage(amount);
        }
    }

}
