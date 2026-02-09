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
    [SerializeField] private GameObject dialogueBox;
    private ResponseHandler responseHandler;


    void Start()
    {
        scrollingText = GetComponent<ScrollingText>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogue();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        DialogueManager.Instance.OpenDialogue();
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return scrollingText.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space));

        }
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogue();
        }
    }

    private void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        DialogueManager.Instance.CloseDialogue();
    }

}
