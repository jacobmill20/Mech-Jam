using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject following;
    public float buffer;

    // Update is called once per frame
    void Update()
    {
        float followX = following.transform.position.x;
        if(followX - transform.position.x > buffer)
            transform.position = new Vector3(following.transform.position.x - buffer, 0.4f, -50f);
        if (followX - transform.position.x < -buffer)
            transform.position = new Vector3(following.transform.position.x + buffer, 0.4f, -50f);
    }
}
