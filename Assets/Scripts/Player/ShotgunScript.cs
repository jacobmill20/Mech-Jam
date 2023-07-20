using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    public GameObject bulletContainer, projectile;
    public GameObject[] attackPoints;
    public float shootInterval;
    public int damage;

    private SpriteRenderer spriteRenderer;
    private bool attackPointFlipped;
    private float shootTimer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Shoot();
    }

    private void Rotate()
    {
        Vector3 diffference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diffference.Normalize();
        float rotationZ = Mathf.Atan2(diffference.y, diffference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            spriteRenderer.flipY = true;
            spriteRenderer.sortingOrder = 6;
        }
        else
        {
            spriteRenderer.flipY = false;
            spriteRenderer.sortingOrder = 8;
        }

        //Flip Attack point
        if (attackPointFlipped ^ spriteRenderer.flipY)
        {
            foreach (GameObject a in attackPoints)
            {
                Vector2 pos = a.transform.localPosition;
                a.transform.localPosition = new Vector2(pos.x, -pos.y);
            }
            attackPointFlipped = !attackPointFlipped;
        }
    }

    private void Shoot()
    {
        //If locked on and fire interval waited, shoot
        if (Input.GetMouseButton(0) && shootTimer >= shootInterval)
        {
            foreach (GameObject a in attackPoints)
            {
                //Spawn projectiles
                for (int i = 0; i < 4; i++)
                {
                    Quaternion projRotation = a.transform.rotation;
                    projRotation.eulerAngles += new Vector3(0f, 0f, Random.Range(-6f, 6f));
                    GameObject newBullet = Instantiate(projectile, a.transform.position, projRotation, bulletContainer.transform);
                    newBullet.GetComponent<BulletScript>().damage = damage;
                }
            }
            shootTimer = 0f;
        }

        //Add to timer
        if (shootTimer < shootInterval)
            shootTimer += Time.deltaTime;
    }
}
