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
        mySprite.sprite = powerLevels[idx];
    }
}
