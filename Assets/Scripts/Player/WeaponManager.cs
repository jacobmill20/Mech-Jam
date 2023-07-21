using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject activeGun;
    
    public void SwapGuns(int newGun)
    {
        activeGun.SetActive(false);

        transform.GetChild(newGun).gameObject.SetActive(true);

        activeGun = transform.GetChild(newGun).gameObject;

        activeGun.SetActive(true);
    }
}
