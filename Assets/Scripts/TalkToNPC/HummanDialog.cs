using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Unity.VisualScripting;

public class HummanDialog : MonoBehaviour, IDialogSystem
{
    public string[] dialogReplics;
    public GameObject dialogUI;
    public int dialogIndex = 0;
    private Animator animator;

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

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartDialog()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Run",false);
        DialogUI.SetActive(true);
        if (DialogIndex < DialogReplics.Length)
        {
            DialogUI.GetComponentInChildren<TextMeshProUGUI>().text = DialogReplics[DialogIndex];
        }
    }
    public void NextReplics()
    {
        if (dialogIndex < DialogReplics.Length)
        {
            DialogIndex++;

            DialogUI.GetComponentInChildren<TextMeshProUGUI>().text = DialogReplics[DialogIndex];
        }
    }

    public void EndDialog()
    {
        DialogIndex = 0;
        DialogUI.GetComponentInChildren<TextMeshProUGUI>().text = "";
        DialogUI.SetActive(false);
    }
}
