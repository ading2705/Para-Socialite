using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public static DialogueManager Instance { get { return _instance; } }

    private bool isDialogueOpen;

    public bool IsDialogueOpen => isDialogueOpen;

    void Start()
    {
        _instance = this;
        CloseDialogue();
    }

    public void OpenDialogue()
    {
        isDialogueOpen = true;
    }

    public void CloseDialogue()
    {
        isDialogueOpen = false;
    }

}
