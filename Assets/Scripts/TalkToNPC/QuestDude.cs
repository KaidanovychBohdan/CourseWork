using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDude : MonoBehaviour, IDialogSystem
{
    public string[] dialogReplics;
    public GameObject dialogUI;
    public int dialogIndex = 0;

    private bool isComplete = false;

    public string[] DialogReplics
    {
        get { return dialogReplics; }
        set { dialogReplics = value; }
    }

    public GameObject DialogUI
    {
        get { return dialogUI; }
        set { dialogUI = value; }
    }

    public int DialogIndex
    {
        get { return dialogIndex; }
        set { dialogIndex = value; }
    }

    private void FixedUpdate()
    {
        if (isComplete)
        {
            // як≥сь додатков≥ д≥њ, €к≥ потр≥бно виконати, коли квест завершено
        }
    }

    public void StartDialog()
    {
        DialogUI.SetActive(true);
        if (DialogIndex < DialogReplics.Length)
        {
            DialogUI.GetComponentInChildren<TextMeshProUGUI>().text = DialogReplics[DialogIndex];
        }
    }

    public void NextReplics()
    {
        DialogIndex++;

        if (DialogIndex < DialogReplics.Length)
            DialogUI.GetComponentInChildren<TextMeshProUGUI>().text = DialogReplics[DialogIndex];
    }

    public void EndDialog()
    {
        DialogIndex = 0;
        DialogUI.GetComponentInChildren<TextMeshProUGUI>().text = "";
        DialogUI.SetActive(false);
    }

}
