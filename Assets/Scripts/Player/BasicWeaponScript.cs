using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeaponScript : MonoBehaviour
{
    public GameObject bulletContainer, attackPoint, projectile;
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
            Vector2 pos = attackPoint.transform.localPosition;
            attackPoint.transform.localPosition = new Vector2(pos.x, -pos.y);
            attackPointFlipped = !attackPointFlipped;
        }
    }

    private void Shoot()
    {
        //If locked on and fire interval waited, shoot
        if (Input.GetMouseButton(0) && shootTimer >= shootInterval)
        {
            GameObject newBullet = Instantiate(projectile, attackPoint.transform.position, attackPoint.transform.rotation, bulletContainer.transform);
            newBullet.GetComponent<BulletScript>().damage = damage;
            shootTimer = 0f;
        }

        //Add to timer
        if(shootTimer < shootInterval)
            shootTimer += Time.deltaTime;
    }
}
