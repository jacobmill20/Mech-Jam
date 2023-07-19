using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public float rotationSpeed, shootInterval;
    public int maxHealth, health;
    
    [SerializeField] private GameObject turretHead;
    [SerializeField] private GameObject battery;
    [SerializeField] private RangeBoxScript rangeBox;
    [SerializeField] private GameObject destroyedTurret;

    private GameObject target;
    private bool lockedOn;
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
            Vector3 diffference = target.transform.position - turretHead.transform.position;
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
            turretHead.GetComponent<IShootable>().Shoot();
            shootTimer = 0f;
        }

        //Add to timer
        shootTimer += Time.deltaTime;
    }

    private void DestroyTurret()
    {
        //Create new dead turret then disable turret
        GameObject deadGuy = Instantiate(destroyedTurret, transform.position, transform.rotation);
        deadGuy.GetComponent<DestroyedTurretScript>().turret = gameObject;
        gameObject.SetActive(false);
    }
}
