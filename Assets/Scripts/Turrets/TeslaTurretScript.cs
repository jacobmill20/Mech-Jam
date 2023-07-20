using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaTurretScript : MonoBehaviour, IShootable
{
    public float despawnTime;
    public Transform attackPoint;
    public GameObject[] arcs;
    public RangeBoxScript rangeBox;
    public GameObject projectileContainer;

    private List<GameObject> damageArcs;

    void Start()
    {
        damageArcs = new List<GameObject>();
    }

    public void Shoot(int damage)
    {
        foreach (GameObject t in rangeBox.targets)
        {
            //Calculate length of arc
            float distance = Vector2.Distance(attackPoint.position, t.transform.position);
            int idx = (int)Mathf.Floor((distance / 8.5f) * 6f);

            GameObject arc = Instantiate(arcs[idx], attackPoint.position, attackPoint.rotation, projectileContainer.transform);
            damageArcs.Add(arc);

            //Calculate target angle
            Vector3 diffference = t.transform.position - attackPoint.transform.position;
            diffference.Normalize();
            float rotationZ = Mathf.Atan2(diffference.y, diffference.x) * Mathf.Rad2Deg;
            arc.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

            StartCoroutine(DespawnArcs());

            //Damage the target somehow
            t.GetComponent<EnemyController>().health -= damage;
        }
    }

    private IEnumerator DespawnArcs()
    {
        yield return new WaitForSeconds(despawnTime);

        foreach(GameObject a in damageArcs)
        {
            Destroy(a);
        }
    }
}
