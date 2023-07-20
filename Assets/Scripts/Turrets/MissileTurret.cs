using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : MonoBehaviour, IShootable
{
    public float missileSpeed;
    public Transform attackPoint;
    public GameObject projectile, projectileContainer;

    private GameObject activeMissile;
    private bool readyToFire;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Relaod());
    }

    public void Shoot(int damage)
    {
        if (readyToFire)
        {
            activeMissile.GetComponent<Animator>().SetTrigger("Fly");
            activeMissile.GetComponent<MissileScript>().target = gameObject.transform.parent.GetComponent<TurretScript>().target;
            activeMissile.GetComponent<BulletScript>().speed = missileSpeed;
            activeMissile.GetComponent<BulletScript>().damage = damage;
            activeMissile.transform.parent = projectileContainer.transform;
            readyToFire = false;
            StartCoroutine(Relaod());
        }
    }

    private IEnumerator Relaod()
    {
        yield return new WaitForSeconds(1f);

        activeMissile = Instantiate(projectile, attackPoint.position, attackPoint.rotation, attackPoint.transform);
        activeMissile.transform.localScale = Vector3.zero;

        while(activeMissile.transform.localScale.x < 1f)
        {
            yield return new WaitForSeconds(0.01f);
            activeMissile.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        }

        readyToFire = true;
    }
}
