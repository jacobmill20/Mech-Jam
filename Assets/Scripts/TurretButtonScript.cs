using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretButtonScript : MonoBehaviour
{
    public static TurretButtonScript instance;
    
    public Button[] turretButtons;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void DisableButtons()
    {
        for(int i = 0; i < turretButtons.Length; i++)
        {
            turretButtons[i].interactable = false;
        }
    }

    private void EnableButtons()
    {
        for (int i = 0; i < turretButtons.Length; i++)
        {
            turretButtons[i].interactable = true;
        }
    }
}
