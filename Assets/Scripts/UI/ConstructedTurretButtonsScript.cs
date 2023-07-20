using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConstructedTurretButtonsScript : MonoBehaviour
{
    public static ConstructedTurretButtonsScript instance;

    public GameObject E, TurretSpot;
    public Button[] Buttons;
    public TMP_Text RepairPriceText;
    public TMP_Text SellPriceText;

    private Animator anim;
    private GameObject ActiveTurret;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //move the buttons with the active turret on screen
        if (ActiveTurret)
        {
            transform.position = Camera.main.GetComponent<Camera>().WorldToScreenPoint(ActiveTurret.transform.position) + new Vector3(0f, 270f, 0f);
            RepairPriceText.text = ActiveTurret.GetComponent<TurretScript>().GetRepairPrice().ToString();
        }
    }

    public void ActivateTurret(GameObject turret)
    {
        //Save the turret that was activated then show buttons
        ActiveTurret = turret;
        SellPriceText.text = turret.GetComponent<TurretScript>().GetSellPrice().ToString();
        ShowButtons();
        anim.SetBool("Active", true);
    }

    public void DeactivateTurret()
    {
        //Play button disable animation
        anim.SetBool("Active", false);
    }

    public void RepairTurret()
    {
        TurretScript turret = ActiveTurret.GetComponent<TurretScript>();
        if (EconomySystem.instance.CheckPrice(turret.GetRepairPrice()))
        {
            turret.health = turret.maxHealth;
            RepairPriceText.text = turret.GetRepairPrice().ToString();
        }  
    }

    public void SellTurret()
    {
        TurretScript turret = ActiveTurret.GetComponent<TurretScript>();
        EconomySystem.instance.AddMoney(turret.GetSellPrice());

        DeactivateTurret();
        Instantiate(TurretSpot, ActiveTurret.transform.position, ActiveTurret.transform.rotation);
        Destroy(ActiveTurret);
    }

    private void HideButtons()
    {
        //Hide Buttons
        if (!anim.GetBool("Active"))
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].gameObject.SetActive(false);
            }

            E.SetActive(false);
        }

    }

    private void ShowButtons()
    {
        //Show buttons
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].gameObject.SetActive(true);
        }

        //E.SetActive(true);
    }

    private void DisableButtonsInteract()
    {
        //Make buttons not interactable
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].interactable = false;
        }

        E.SetActive(false);
    }

    private void EnableButtonsInteract()
    {
        //Make buttons interactable if unlocked
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].interactable = true;
        }

        E.SetActive(true);
    }
}
