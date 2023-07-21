using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour
{
    public Sprite[] powerLevels;
    public int maxHealth, health;

    private SpriteRenderer mySprite;

    private void Awake()
    {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    private void Update()
    {
        UpdateHealth();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy projectile
        if (collision.gameObject.layer == 9)
        {
            //Store Bullet Script
            BulletScript bulletScript = collision.gameObject.GetComponent<BulletScript>();
            //Subtract health
            health -= bulletScript.damage;
            //Destroy projectile
            if (!bulletScript.undestroyable)
                Destroy(collision.gameObject);
        }
    }

    public void UpdateHealth()
    {
        if (health <= 0)
            GameManager.instance.EndGame();
        int idx = (health * 10) / maxHealth;

        if (idx >= 0 && idx < powerLevels.Length)
            mySprite.sprite = powerLevels[idx];
    }
}
