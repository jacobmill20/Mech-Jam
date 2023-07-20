using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float damage;
    
    [SerializeField] private RangeBoxScript rangeBox;
    [SerializeField] private GameObject turretSpot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            GetComponent<Animator>().SetTrigger("Splode");
            transform.Translate(new Vector3(0f, 1.28f, 0f));
        }

        //Deal damage to everything in range box
    }

    public void DestroyMine()
    {
        Instantiate(turretSpot, transform.parent.position, transform.parent.rotation, transform.parent.transform.parent);
        Destroy(transform.parent.gameObject);
    }
}
