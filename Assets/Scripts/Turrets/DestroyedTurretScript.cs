using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedTurretScript : MonoBehaviour
{
    public GameObject turret, E;

    private bool playerPresent, active;

    private void Update()
    {
        //If the player is standing at the turret spot and presses E, activate the spot. If already active, deactivate
        //**Active means that the construction buttons for the spot are showing
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

    public void RepairTurret()
    {
        turret.SetActive(true);
        turret.GetComponent<TurretScript>().health = turret.GetComponent<TurretScript>().maxHealth;
        Destroy(gameObject);
    }

    private void Activate()
    {
        DestroyedButtonScript.instance.ActivateTurret(gameObject);
        active = true;

        E.SetActive(false);
    }

    private void Deactivate()
    {
        DestroyedButtonScript.instance.DeactivateTurret();
        active = false;

        if (playerPresent)
            E.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
}
