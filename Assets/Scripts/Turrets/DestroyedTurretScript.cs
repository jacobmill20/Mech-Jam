using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedTurretScript : MonoBehaviour
{
    public GameObject turret;
    public bool repair;

    private void Update()
    {
        if (repair)
            RepairTurret();
    }

    public void RepairTurret()
    {
        turret.SetActive(true);
        turret.GetComponent<TurretScript>().health = turret.GetComponent<TurretScript>().maxHealth;
        Destroy(gameObject);
    }
}
