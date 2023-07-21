using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButtonScript : MonoBehaviour
{
    public Button equip;
    
    public void PurchaseButton(int price)
    {
        if (EconomySystem.instance.CheckPrice(price))
        {
            equip.interactable = true;
            gameObject.SetActive(false);
        }
    }
}
