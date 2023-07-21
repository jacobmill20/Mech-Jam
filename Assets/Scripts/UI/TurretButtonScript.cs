using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretButtonScript : MonoBehaviour
{
    public static TurretButtonScript instance;

    public GameObject E;
    public Button[] turretButtons;

    private Animator anim;
    private GameObject ActiveSpot;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //move the buttons with the active spot on screen
        if (ActiveSpot)
        {
            transform.position = Camera.main.GetComponent<Camera>().WorldToScreenPoint(ActiveSpot.transform.position) + new Vector3(0f, 200f, 0f);
        }
    }

    public void ActivateSpot(GameObject spot)
    {
        //Save the spot that was activated then show buttons
        ActiveSpot = spot;
        ShowButtons();
        anim.SetBool("Active", true);
    }

    public void DeactivateSpot()
    {
        //Play button disable animation
        anim.SetBool("Active", false);
    }

    public void ConstructTurret(GameObject turret)
    {
        //If have enough money
        int price;
        if(turret.TryGetComponent<TurretScript>(out TurretScript turretScript))
        {
            price = turretScript.price;
        } else
        {
            price = 1500;
        }

        if (EconomySystem.instance.CheckPrice(price))
        {
            //Deactivate the spot, hide it, then place a turret
            DeactivateSpot();
            ActiveSpot.SetActive(false);
            GameObject newTurret = Instantiate(turret, ActiveSpot.transform.position, Quaternion.identity);
            if (ActiveSpot.GetComponent<TurretSpotScript>().isFacingLeft && !newTurret.name.Contains("Mine"))
            {
                newTurret.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                newTurret.GetComponent<TurretScript>().isFacingLeft = true;
            }
        }
    }
    
    private void HideButtons()
    {
        //Hide Buttons
        if (!anim.GetBool("Active"))
        {
            for (int i = 0; i < turretButtons.Length; i++)
            {
                turretButtons[i].gameObject.SetActive(false);
            }

            E.SetActive(false);
        }
        
    }

    private void ShowButtons()
    {
        //Show buttons
        for (int i = 0; i < turretButtons.Length; i++)
        {
            turretButtons[i].gameObject.SetActive(true);
        }

        //E.SetActive(true);
    }

    private void DisableButtonsInteract()
    {
        //Make buttons not interactable
        for(int i = 0; i < turretButtons.Length; i++)
        {
            turretButtons[i].interactable = false;
        }

        E.SetActive(false);
    }

    private void EnableButtonsInteract()
    {
        //Make buttons interactable if unlocked
        for (int i = 0; i < turretButtons.Length; i++)
        {
             turretButtons[i].interactable = true;
        }

        E.SetActive(true);
    }

}
