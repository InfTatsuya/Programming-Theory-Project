using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectSpeed = 3000.0f;

    [SerializeField] protected int damage;
    public int Damage // ENCAPSULATION
    {
        get { return damage; }
    }

    [SerializeField] protected float range = 1000.0f;
    public float Range // ENCAPSULATION
    {
        get { return range; }
    }

    float lifeTime;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        lifeTime = range / projectSpeed;

        StartCoroutine(DestroyOverTime());
    }

    IEnumerator DestroyOverTime()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(transform.parent.gameObject);
    }

    private void Update()
    {
        rb.velocity = transform.up * projectSpeed;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Enemy" || other.gameObject.tag == "Player")
        {
            Destroy(transform.parent.gameObject);
            
            Actor actor = other.gameObject.GetComponent<Actor>();
            if(actor != null)
            {
                actor.TakeDamage(damage);
            }
        }
    }
}
