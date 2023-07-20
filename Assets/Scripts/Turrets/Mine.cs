using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int damage;
    
    [SerializeField] private RangeBoxScript rangeBox;
    [SerializeField] private GameObject turretSpot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            GetComponent<Animator>().SetTrigger("Splode");

            //Deal damage to everything in range box
            foreach (GameObject t in rangeBox.targets)
            {
                t.GetComponent<EnemyController>().health -= damage;
            }
        }
    }

    public void DestroyMine()
    {
        Instantiate(turretSpot, transform.parent.position, transform.parent.rotation, transform.parent.transform.parent);
        Destroy(transform.parent.gameObject);
    }
}
