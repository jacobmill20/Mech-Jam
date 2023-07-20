using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public float rotationSpeed, shootInterval;
    public int price, maxHealth, health, damage;
    public bool isFacingLeft;
    
    [SerializeField] private GameObject turretHead;
    [SerializeField] private GameObject battery;
    [SerializeField] private RangeBoxScript rangeBox;
    [SerializeField] private GameObject destroyedTurret, E;

    public GameObject target { get; private set; }
    private bool lockedOn, playerPresent, active;
    private float shootTimer;

    private void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        FindClosestTarget();
        RotateHead();
        Shoot();
        CheckForActivate();
    }

    private void UpdateHealth()
    {
        if (health <= 0)
            DestroyTurret();
        battery.GetComponent<BatteryScript>().UpdateHealth((health * 10) / maxHealth);
    }

    private void FindClosestTarget()
    {
        //Initialize Target
        if(rangeBox.targets.Count == 0)
        {
            target = null;
        } else
        {
            target = rangeBox.targets[0];
        }
        
        //Check each target in range to see if it is closer
        foreach (GameObject t in rangeBox.targets)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > Vector2.Distance(transform.position, t.transform.position))
                target = t;
        }
    }

    private void RotateHead()
    {
        if(target is not null)
        {
            //Calculate target angle
            Vector3 targetPos;
            if (isFacingLeft)
            {
                targetPos = target.transform.position - new Vector3(2f * (target.transform.position.x - turretHead.transform.position.x), 0f, 0f);
            } else
            {
                targetPos = target.transform.position;
            }

            Vector3 diffference = targetPos - turretHead.transform.position;
            diffference.Normalize();
            float rotationZ = Mathf.Atan2(diffference.y, diffference.x) * Mathf.Rad2Deg;
            //turretHead.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);  //Instant targetting

            //A bit of conversion because rotation is wierd
            float convertedRot;
            if(turretHead.transform.eulerAngles.z > 180f)
            {
                convertedRot = turretHead.transform.eulerAngles.z - 360;
            } else
            {
                convertedRot = turretHead.transform.eulerAngles.z;
            }

            //Gradual targetting
            if(convertedRot > rotationZ + 1.5f)
            {
                turretHead.transform.Rotate(new Vector3(0f, 0f, -rotationSpeed * Time.deltaTime));
                lockedOn = false;
            } else if (convertedRot < rotationZ - 1.5f)
            {
                turretHead.transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime));
                lockedOn = false;
            } else
            {
                lockedOn = true;
            }
        } else
        {
            lockedOn = false;
        }
    }

    private void Shoot()
    {
        //If locked on and fire interval waited, shoot
        if(lockedOn && shootTimer >= shootInterval)
        {
            turretHead.GetComponent<IShootable>().Shoot(damage);
            shootTimer = 0f;
        }

        //Add to timer
        if (shootTimer < shootInterval)
            shootTimer += Time.deltaTime;
    }

    private void DestroyTurret()
    {
        //Create new dead turret then disable turret
        GameObject deadGuy = Instantiate(destroyedTurret, transform.position, transform.rotation);
        deadGuy.GetComponent<DestroyedTurretScript>().turret = gameObject;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy projectile
        if(collision.gameObject.layer == 9)
        {
            //Store Bullet Script
            BulletScript bulletScript = collision.gameObject.GetComponent<BulletScript>();
            //Subtract health
            health -= bulletScript.damage;
            //Destroy projectile
            if (!bulletScript.undestroyable)
                Destroy(collision.gameObject);
        }

        if (collision.tag == "Player")
        {
            playerPresent = true;
            E.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerPresent = false;
            E.SetActive(false);

            if (active)
            {
                Deactivate();
            }
        }
    }

    public int GetRepairPrice()
    {
        if (health < 0)
            health = 0;

        return (int)((1f - (float)health / maxHealth) * price * 0.8f);
    }

    public int GetSellPrice()
    {
        return (int)(price * 0.5f);
    }

    public void CheckForActivate()
    {
        if (playerPresent && Input.GetKeyDown(KeyCode.E))
        {
            if (!active)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }
    }

    private void Activate()
    {
        ConstructedTurretButtonsScript.instance.ActivateTurret(gameObject);
        active = true;

        E.SetActive(false);
    }

    private void Deactivate()
    {
        ConstructedTurretButtonsScript.instance.DeactivateTurret();
        active = false;

        if (playerPresent)
            E.SetActive(true);
    }
}
