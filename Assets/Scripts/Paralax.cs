using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public GameObject[] backgrounds;
    public float offsetAmountX, offsetAmountY;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < backgrounds.Length; i++)
        {
            //Horizontal offset
            Vector2 offset = new Vector2(i * offsetAmountX * transform.position.x, 0f);
            backgrounds[i].GetComponent<Renderer>().material.mainTextureOffset = offset;

            //Vertical offset
            Vector3 currentPos = backgrounds[i].transform.localPosition;
            backgrounds[i].transform.localPosition = new Vector3(currentPos.x, i * offsetAmountY * transform.position.y, currentPos.z);
        }
    }
}
