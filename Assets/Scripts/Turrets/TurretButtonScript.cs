using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretButtonScript : MonoBehaviour
{
    public static TurretButtonScript instance;
    
    public Button[] turretButtons;
    public bool[] unlocks;

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
            transform.position = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(ActiveSpot.transform.position);
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
        //Deactivate the spot, hide it, then place a turret
        DeactivateSpot();
        ActiveSpot.SetActive(false);
        Instantiate(turret, ActiveSpot.transform.position, Quaternion.identity);
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
        }
        
    }

    private void ShowButtons()
    {
        //Show buttons
        for (int i = 0; i < turretButtons.Length; i++)
        {
            turretButtons[i].gameObject.SetActive(true);
        }
    }

    private void DisableButtonsInteract()
    {
        //Make buttons not interactable
        for(int i = 0; i < turretButtons.Length; i++)
        {
            turretButtons[i].interactable = false;
        }
    }

    private void EnableButtonsInteract()
    {
        //Make buttons interactable if unlocked
        for (int i = 0; i < turretButtons.Length; i++)
        {
            if(unlocks[i] == true)
                turretButtons[i].interactable = true;
        }
    }

}
