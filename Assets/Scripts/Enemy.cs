using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Actor
{
    [SerializeField] GameObject mesh;
    [SerializeField] Rigidbody rb;
    [SerializeField] SphereCollider triggerCollider;

    [SerializeField] float visionRange = 150.0f;
    [SerializeField] float fireRate = 1.0f;

    [SerializeField] Transform target;
    [SerializeField] Transform patrolPoint;

    NavMeshAgent agent;
    bool hasTarget = false;

    bool canShoot = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if(patrolPoint != null)
        {
            patrolPoint = transform;
        }
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }

    protected override void Fire()
    {
        
    }

    protected override void Move()
    {
        if (!canMove)
        {
            //TODO: for turret;
        }
        else
        {
            if (hasTarget)
            {
                agent.SetDestination(target.transform.position);
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if(distance <= projectile.GetComponent<Projectile>().Range)
                {
                    agent.isStopped = true;
                    canShoot = true;
                }
            }
        }
    }

    protected override void RotateTower()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = other.transform;
            hasTarget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
            hasTarget = false;
        }
    }
}
