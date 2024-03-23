using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestDude : MonoBehaviour, IDialogSystem
{
    public GameObject respawnPos;
    public string[] dialogReplics;
    public GameObject dialogUI;
    public GameObject button;
    public int dialogIndex = 0;
    private GameObject quests;

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

    private void Start()
    {
        quests = GameObject.Find("Quest");
        dialogUI = GameObject.Find("DialogPanel");
        button = GameObject.Find("Accept");
        StartCoroutine(WaitForASec());
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
    private IEnumerator WaitForASec()
    {
        yield return new WaitForSeconds(2f);
        quests.SetActive(false);
        button.SetActive(false);
        dialogUI.SetActive(false);
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
        DialogUI.SetActive(false);
        respawnPos.SetActive(true);
        quests.SetActive(true);
        SceneManager.LoadScene(2);
    }

    public void EndDialog()
    {
        respawnPos.SetActive(false);
        DialogIndex = 0;
        DialogUI.GetComponentInChildren<TextMeshProUGUI>().text = "";
        DialogUI.SetActive(false);
    }

}
