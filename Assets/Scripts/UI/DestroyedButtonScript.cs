using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DestroyedButtonScript : MonoBehaviour
{
    public static DestroyedButtonScript instance;

    public GameObject E;
    public Button[] Buttons;
    public TMP_Text RepairPriceText;

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
        }
    }

    public void ActivateTurret(GameObject turret)
    {
        //Save the turret that was activated then show buttons
        ActiveTurret = turret;
        RepairPriceText.text = turret.GetComponent<DestroyedTurretScript>().turret.GetComponent<TurretScript>().GetRepairPrice().ToString();
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
        TurretScript turret = ActiveTurret.GetComponent<DestroyedTurretScript>().turret.GetComponent<TurretScript>();
        if (EconomySystem.instance.CheckPrice(turret.GetRepairPrice()))
        {
            ActiveTurret.GetComponent<DestroyedTurretScript>().RepairTurret();
        }
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
