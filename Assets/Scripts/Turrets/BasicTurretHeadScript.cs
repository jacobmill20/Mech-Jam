using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurretHeadScript : MonoBehaviour, IShootable
{
    public List<Transform> attackPoints;
    public GameObject projectile, projectileContainer;

    private int attackIdx;

    private void Awake()
    {
        attackIdx = 0;
    }
    
    public void Shoot(int damage)
    {
        //Spawn projectile
        GameObject newBullet = Instantiate(projectile, attackPoints[attackIdx].position, attackPoints[attackIdx].rotation, projectileContainer.transform);
        newBullet.GetComponent<BulletScript>().damage = damage;

        //Increment attack idx
        attackIdx++;
        if (attackIdx > attackPoints.Count - 1)
            attackIdx = 0;
    }
}
