using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    #region VARIABLES
    public float force, jumpForce, airPenalty;
    public int health;
    public static PlayerMovementScript instance;
    public GameObject groundCheckPosition;
    public LayerMask groundLayer;
    public GameObject healthBar;

    private float speed;
    private bool isGround, isDead;
    private Vector3 mousePostition, prevPosition;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D mybody;
    private Animator anim;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mybody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        prevPosition = transform.position;

        if (instance == null)
            instance = this;
    }

    void Update()
    {
        CheckIfGrounded();
        MovePlayer();
        Animate();
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
        //Set speed depending on if the player is in the air or not, change velocity if player is on the ground, add force if player is in the air
        float dirX = Input.GetAxis("Horizontal");
        if (!isGround)
        {
            speed = force * airPenalty;
            if ((dirX > 0 && mybody.velocity.x < speed) || (dirX < 0 && mybody.velocity.x > -speed)) //Only add force if velocity is less than speed
                mybody.AddForce(new Vector2(dirX * speed, 0f));
        }
        else
        {
            speed = force;
            mybody.velocity = new Vector2(dirX * speed, mybody.velocity.y);
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            mybody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }

    private void Animate()
    {
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

        //If falling play falling anim
        if(mybody.velocity.y < -1)
        {
            anim.SetBool("Fall", true);
        } else
        {
            anim.SetBool("Fall", false);
        }
        
        //left and right movement
        if (mybody.velocity.x > 1)
        {
            if(mousePostition.x > transform.position.x)
            {
                anim.SetBool("Walk", true);
                anim.SetBool("WalkBack", false);
            } else if (mousePostition.x < transform.position.x)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("WalkBack", true);
            }
        } else if (mybody.velocity.x < -1)
        {
            if (mousePostition.x > transform.position.x)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("WalkBack", true);
            }
            else if (mousePostition.x < transform.position.x)
            {
                anim.SetBool("Walk", true);
                anim.SetBool("WalkBack", false);
            }
        } else {
            anim.SetBool("Walk", false);
            anim.SetBool("WalkBack", false);
        } 
    }

    public void CheckIfGrounded()
    {
        isGround = Physics2D.Raycast(groundCheckPosition.transform.position, Vector2.down, 0.01f, groundLayer);
    }

    public bool GetIsDead()
    {
        return isDead;
    }

}