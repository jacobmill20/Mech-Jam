using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject bullet;
    public GameObject bulletContainer;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PlayerMovementScript.instance.GetIsDead())
        {
            Vector3 diffference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diffference.Normalize();
            float rotationZ = Mathf.Atan2(diffference.y, diffference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            {
                spriteRenderer.flipY = true;
            }
            else
            {
                spriteRenderer.flipY = false;
            }
        }
    }

    void Update()
    {
        if (!PlayerMovementScript.instance.GetIsDead())
            fireBullet();
    }

    private void fireBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation, bulletContainer.transform);
            //SoundManager.instance.PlayShot();
        }
    }

}
