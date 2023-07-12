using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour
{
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);

        if(collision.gameObject.tag == "StartGame")
        {
            SceneManager.LoadScene("SampleScene");
        }
        if(collision.gameObject.tag == "Quit")
        {
            Application.Quit();
        }
    }
}
