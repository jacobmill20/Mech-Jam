using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float fireRate, speed;
    public int maxHealth, health;
    public bool isFacingLeft;

    [SerializeField] private EnemyRange range;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject projectile, container;

    public GameObject target { get; private set; }
    private bool lockedOn = false;
    private float shootTimer;

    private GameObject activeMissile;

    private Animator anim;

    private void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        UpdateHealth();
        FindClosestTarget();
        Shoot();
        MoveTank();
    }

    private void MoveTank()
    {
        if(target is null)
        {
            anim.SetBool("Driving", true);
            transform.position += transform.right * speed * Time.deltaTime;

        }
    }
    private void UpdateHealth()
    {
        if (health <= 0)
            DestroyTank();
    }

    private void FindClosestTarget()
    {
        //Initialize Target
        if (range.targets.Count == 0)
        {
            lockedOn = false;
            target = null;
        }
        else
        {   
            target = range.targets[0];
            attackPoint.LookAt(target.transform);
            lockedOn = true;
        }

        //Check each target in range to see if it is closer
        foreach (GameObject t in range.targets)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > Vector2.Distance(transform.position, t.transform.position))
                target = t;
        }
    }

    private void Shoot()
    {
        //If locked on and fire interval waited, shoot
        if (lockedOn && shootTimer >= fireRate)
        {
            activeMissile = Instantiate(projectile, attackPoint.position, attackPoint.rotation, container.transform);
            activeMissile.GetComponent<Animator>().SetTrigger("Fly");
            activeMissile.GetComponent<MissileScript>().target = target;

            shootTimer = 0f;
        }

        //Add to timer
        if (shootTimer < fireRate)
            shootTimer += Time.deltaTime;
    }

    private void DestroyTank()
    {
        //Create new dead turret then disable turret
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            //Store Bullet Script
            BulletScript bulletScript = collision.gameObject.GetComponent<BulletScript>();
            //Subtract health
            health -= bulletScript.damage;
            //Destroy projectile
            if (!bulletScript.undestroyable)
                Destroy(collision.gameObject);
        }
    }
}
