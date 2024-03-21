using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerInteract : MonoBehaviour 
{
    public GameObject pressFBTN;
    private bool dialogStart = false;

    public object TypeOfDude { get; private set; }

    private void OnTriggerEnter(Collider collider)
    {
        pressFBTN.SetActive(true);
    }
    private void OnTriggerStay(Collider collider)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dialogStart = true;
            pressFBTN.SetActive(false);
            if (collider)
            {
                collider.gameObject.GetComponent<HummanDialog>().StartDialog();
            }
            else 
            {
               collider.gameObject.GetComponent<QuestDude>().StartDialog();
            }
        }
        if (dialogStart)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                collider.gameObject.GetComponent<HummanDialog>().NextReplics();
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        pressFBTN.SetActive(false);
        collider.gameObject.GetComponent<HummanDialog>().EndDialog(); 
        dialogStart = false;
    }
}