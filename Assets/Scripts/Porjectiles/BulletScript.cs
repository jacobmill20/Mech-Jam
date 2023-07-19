using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public int damage;
    public bool undestroyable;

    private float flightTime = 5f;

    // Update is called once per frame
    void Update()
    {
        Move();

        flightTime -= Time.deltaTime;
        if(flightTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
