using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour /// INHERITANCE - parent class
{
    [SerializeField] protected int healthMax;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected ParticleSystem impactEffect;
    [SerializeField] ParticleSystem deathEffect;

    [SerializeField] protected float speed;

    [SerializeField] protected bool canMove = true;
    
    
    [SerializeField] protected GameObject tower;
    [SerializeField] protected Transform barrel;


    [SerializeField] protected GameObject projectile;
    [SerializeField] protected ParticleSystem muzzleEffect;

    abstract protected void Move(); // ABSTRACTION

    abstract protected void RotateTower(); // ABSTRACTION
    abstract protected void Fire(); // ABSTRACTION


    protected void Awake()
    {
        currentHealth = healthMax;
    }
    public virtual void TakeDamage(int damageAmount) //// POLYMORPHISM (in Player.cs)
    {
        if (impactEffect != null)
        {
            impactEffect.Play();
        }

        currentHealth -= damageAmount;
        if (currentHealth - damageAmount < 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    protected virtual void Death()
    {
        if(!deathEffect.isPlaying) deathEffect.Play();
        Destroy(gameObject, 2.0f);
    }

    private void FixedUpdate()
    {
        Move();
        RotateTower();
    }

    private void Update()
    {
        Fire();
    }
}
