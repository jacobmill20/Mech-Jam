using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    #region VARIABLES
    public float force, jumpHeight, airPenalty, speedCap;
    public int health;
    public static PlayerMovementScript instance;
    public GameObject groundCheckPosition;
    public LayerMask groundLayer;
    public GameObject healthBar;

    private float speed;
    private bool isGround, isDead;
    private Vector3 mousePostition;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D mybody;
    private Animator anim;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mybody = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();

        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        MovePlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Recycled code, may or may not use
        /*if(canTakeDamage && collision.gameObject.tag == "Enemy")
        {
            health -= 1;
            canTakeDamage = false;

            Vector3 temp = new Vector3((health / 5f) * 0.4f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            healthBar.transform.localScale = temp;

            if(health > 0)
                SoundManager.instance.PlayerHurt();
                Invoke("ResetDamage", 1f);
            if(health == 0)
            {
                SoundManager.instance.PlayerDie();
                anim.enabled = false;
                isDead = true;
                transform.Rotate(new Vector3(0f, 0f, 90f));
                UIScript.instance.GameOver();
                EnemyControllerScript.instance.StopAllCoroutines();
            }
        }*/
    }

    private void MovePlayer()
    {
        //Set speed depending on if the player is in the air or not
        if (!isGround)
        {
            speed = force * airPenalty;
        }
        else
        {
            speed = force;
        }

        //Flip player sprite depening on the position of the mouse around the player
        mousePostition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePostition.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        //left and right movement
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if(mybody.velocity.x < speedCap)
                mybody.AddForce(Vector2.right * speed);

            /*if(mousePostition.x > transform.position.x)
            {
                anim.SetBool("Walk", true);
                anim.SetBool("WalkBack", false);
            } else if (mousePostition.x < transform.position.x)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("WalkBack", true);
            }*/
        } else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (mybody.velocity.x > -speedCap)
                mybody.AddForce(Vector2.left * speed);

            /*if (mousePostition.x > transform.position.x)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("WalkBack", true);
            }
            else if (mousePostition.x < transform.position.x)
            {
                anim.SetBool("Walk", true);
                anim.SetBool("WalkBack", false);
            }*/
        } else {
            /*anim.SetBool("Walk", false);
            anim.SetBool("WalkBack", false);*/
        }

        //jump
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGround)
            {
                /*
                 * 
                 * Why are we changing velocity of player? This makes it feel less lifelike and isn't affected by gravity as well
                 * Consider changing to addForce for smoothness of play. This will also remove the moon gravity that we have for some reason
                 * 
                */
                mybody.velocity = new Vector3(mybody.velocity.x, jumpHeight, 0f);
            }
        }
    }

    void CheckIfGrounded()
    {
        isGround = Physics2D.Raycast(groundCheckPosition.transform.position, Vector2.down, 0.01f, groundLayer);
    }

    public bool GetIsDead()
    {
        return isDead;
    }

}