using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogSystem
{
    public string[] DialogReplics { get; set; }
    public GameObject DialogUI { get; set; }
    public int DialogIndex { get; set; }

    public void StartDialog();
    public void NextReplics();
    public void EndDialog();
}
