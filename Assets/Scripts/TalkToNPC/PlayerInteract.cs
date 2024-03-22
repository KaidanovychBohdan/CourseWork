using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerInteract : MonoBehaviour 
{
    public GameObject pressFBTN;
    private bool dialogStart = false;
    private IDialogSystem dialogSystem;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Interactable")
            pressFBTN.SetActive(true);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Interactable") 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogStart = true;
                pressFBTN.SetActive(false);
                dialogSystem = collider.gameObject.GetComponent<IDialogSystem>();
                dialogSystem.StartDialog();
            }
            if (dialogStart)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    dialogSystem.NextReplics();
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Interactable")
        {
            pressFBTN.SetActive(false);
            dialogSystem.EndDialog();
            dialogStart = false;
        }
    }
}