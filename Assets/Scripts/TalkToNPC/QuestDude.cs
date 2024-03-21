using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDude : MonoBehaviour, IDialogSystem
{
    public string[] dialogReplics;
    public GameObject dialogUI;
    public GameObject button;
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
            dialogIndex = 2;
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
        if(dialogIndex < DialogReplics.Length)
            DialogIndex++;
        if (!isComplete) 
        {
            if (DialogIndex < DialogReplics.Length - 1)
                DialogUI.GetComponentInChildren<TextMeshProUGUI>().text = DialogReplics[DialogIndex];

            if(dialogIndex == 1) 
            {
                var btn = button.GetComponent<Button>();
                btn.onClick.AddListener(StartQuest);
                button.SetActive(true);
            }
        }
        else 
        {
            DialogUI.GetComponentInChildren<TextMeshProUGUI>().text = DialogReplics[DialogReplics.Length];
        }
    }

    public void StartQuest() 
    {
        var btn = button.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        button.SetActive(false);
        Debug.Log("Переміщення на іншу сцену");
    }

    public void EndDialog()
    {
        DialogIndex = 0;
        DialogUI.GetComponentInChildren<TextMeshProUGUI>().text = "";
        DialogUI.SetActive(false);
    }

}
