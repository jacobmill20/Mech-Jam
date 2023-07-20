using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EconomySystem : MonoBehaviour
{
    public static EconomySystem instance;

    public int money;
    public TMP_Text text;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        UpdateText();
    }

    private void UpdateText()
    {
        text.text = money.ToString();
    }

    public void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        UpdateText();
    }

    public bool CheckPrice(int price)
    {
        if(price <= money)
        {
            money -= price;
            UpdateText();
            return true;
        }
        else
        {
            return false;
        }
    }
}
