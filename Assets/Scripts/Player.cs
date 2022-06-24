using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor //INHERITANCE - child class
{
    [SerializeField] GameObject meshTank;

    [SerializeField] private float steerSpeed = 50.0f;


    Rigidbody rb;

    Vector3 worldPosition;

    bool isInvicible = false;
    [SerializeField] float invincibleTime = 1.0f;

    private void Awake()
    {
        base.Awake();
        canMove = true;
        rb = GetComponentInChildren<Rigidbody>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    protected override void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject proj = Instantiate(projectile, barrel.transform.position, projectile.transform.rotation);

            if(!muzzleEffect.gameObject.activeInHierarchy)
            {
                muzzleEffect.gameObject.SetActive(true);
                Invoke("TurnOffMuzzle", 0.2f);
                muzzleEffect.Play();
            }

            proj.transform.LookAt(worldPosition, Vector3.up);   
            
        }
    }

    void TurnOffMuzzle()
    {
        muzzleEffect.gameObject.SetActive(false);
    }

    protected override void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 moveForce = verticalInput * meshTank.transform.forward * speed * Time.deltaTime;

        rb.AddForce(moveForce, ForceMode.Impulse);

        meshTank.transform.Rotate(0, horizontalInput * steerSpeed * Time.deltaTime, 0);
        
    }

    protected override void RotateTower()
    {
        
        float distance = 100.0f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        worldPosition = new Vector3(ray.GetPoint(distance).x, tower.transform.position.y, ray.GetPoint(distance).z);

        tower.transform.LookAt(worldPosition, Vector3.up);
    }

    public override void TakeDamage(int damageAmount) //// POLYMORPHISM
    {
        if (!isInvicible)
        {
            base.TakeDamage(damageAmount);
            isInvicible = true;

            StartCoroutine(ResetInvincible());
        }
    }

    IEnumerator ResetInvincible()
    {
        yield return new WaitForSeconds(invincibleTime);

        isInvicible = false;
    }
}
