using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public float rotationSpeed, shootInterval;
    
    [SerializeField] private GameObject turretHead;
    [SerializeField] private RangeBoxScript rangeBox;

    private GameObject target;
    private bool lockedOn;
    private float shootTimer;

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        RotateHead();
        Shoot();
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
            float rot;
            if(turretHead.transform.eulerAngles.z > 180f)
            {
                rot = turretHead.transform.eulerAngles.z - 360;
            } else
            {
                rot = turretHead.transform.eulerAngles.z;
            }

            //Gradual targetting
            if(rot > rotationZ + 2f)
            {
                turretHead.transform.Rotate(new Vector3(0f, 0f, -rotationSpeed * Time.deltaTime));
                lockedOn = false;
            } else if (rot < rotationZ - 2f)
            {
                turretHead.transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime));
                lockedOn = false;
            } else
            {
                lockedOn = true;
            }
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
}
