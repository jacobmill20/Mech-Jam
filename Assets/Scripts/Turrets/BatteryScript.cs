using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    public Sprite[] powerLevels;

    private SpriteRenderer mySprite;

    private void Awake()
    {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void UpdateHealth(int idx)
    {
        if(idx >= 0 && idx < powerLevels.Length)
        mySprite.sprite = powerLevels[idx];
    }
}
