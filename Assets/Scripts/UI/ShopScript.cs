using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public WeaponManager wm;

    public void SwapGuns(int newgun)
    {
        wm.SwapGuns(newgun);
    }

    
}
