using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTurretScript : MonoBehaviour, IShootable
{
    public Transform attackPoint;
    public GameObject projectile, projectileContainer;

    public void Shoot(int damage)
    {
        //Spawn projectiles
        for (int i = 0; i < 5; i++)
        {
            Quaternion projRotation = attackPoint.rotation;
            projRotation.eulerAngles += new Vector3(0f, 0f, Random.Range(-5f, 5f));
            GameObject newBullet = Instantiate(projectile, attackPoint.position, projRotation, projectileContainer.transform);
            newBullet.GetComponent<BulletScript>().damage = damage;
        }
    }
}
