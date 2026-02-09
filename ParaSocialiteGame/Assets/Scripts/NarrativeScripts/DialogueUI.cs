using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

// This controls what is displayed in the dialogue box
public class DialogueUI : MonoBehaviour
{
    private ScrollingText scrollingText;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private GameObject dialogueBox;


    void Start()
    {
        scrollingText = GetComponent<ScrollingText>();
        CloseDialogue();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return scrollingText.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space));
        }

        CloseDialogue();
    }

    private void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

}
