using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpotScript : MonoBehaviour
{
    public GameObject E;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            E.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            E.SetActive(false);
        }
    }
}
