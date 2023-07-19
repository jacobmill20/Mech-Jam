using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public List<GameObject> targets { get; private set; }

    private void Awake()
    {
        targets = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Turret")
        {
            targets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Turret")
        {
            targets.Remove(collision.gameObject);
        }
    }
}
