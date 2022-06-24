using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Actor
{
    [SerializeField] GameObject mesh;
    [SerializeField] Rigidbody rb;
    

    [SerializeField] float visionRange = 150.0f;
    [SerializeField] float fireRange = 30.0f;
    [SerializeField] float fireRate = 1.0f;

    [SerializeField] Transform target;
    [SerializeField] Transform patrolPoint;

    SphereCollider triggerCollider;

    NavMeshAgent agent;
    bool hasTarget = false;

    bool canShoot = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        triggerCollider = GetComponentInChildren<SphereCollider>();
    }

    private void Start()
    {
        if(patrolPoint != null)
        {
            patrolPoint = transform;
        }

        triggerCollider.radius = visionRange;
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }

    protected override void Fire()
    {
        if(hasTarget && CheckInFireRange(target.transform.position) && canShoot)
        {
            canShoot = false;

            Invoke("ResetFire", fireRate);

            GameObject proj = Instantiate(projectile, barrel.transform.position, projectile.transform.rotation);

            if (!muzzleEffect.gameObject.activeInHierarchy)
            {
                muzzleEffect.gameObject.SetActive(true);
                Invoke("TurnOffMuzzle", 0.2f);
                muzzleEffect.Play();
            }

            proj.transform.LookAt(target.transform.position, Vector3.up);
        }
    }

    void ResetFire()
    {
        canShoot = true;
    }

    void TurnOffMuzzle()
    {
        muzzleEffect.gameObject.SetActive(false);
    }

    bool CheckInFireRange(Vector3 tarPos)
    {
        return Vector3.Distance(transform.position, tarPos) < fireRange;
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
        if (target == null) return;

        tower.transform.LookAt(target.transform.position, Vector3.up);
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
