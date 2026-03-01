using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    [SerializeField] private RectTransform responseBackground;
    List<GameObject> buttons = new List<GameObject>();
    private string[] sceneChangeResponses = new string[2];

    private DialogueUI dialogueUI;

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    // Builds the response button box
    public void ShowResponses(Response[] responses)
    {
        // note that there will only ever be two responses
        sceneChangeResponses[0] = responses[0].ResponseText;
        sceneChangeResponses[1] = responses[1].ResponseText;
        float responseBoxHeight = 0;
        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            buttons.Add(responseButton);
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {
        responseBox.gameObject.SetActive(false);
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }

        if (response.ResponseText == sceneChangeResponses[0])
        {
            StartCoroutine(DialogueToTransition(response.DialogueObject));
        }
        else
        {
            dialogueUI.ShowDialogue(response.DialogueObject);
        }
    }


    // Force player to wait until response dialogue is finished to transition to new scene
    private IEnumerator DialogueToTransition(DialogueObject dialogue)
    {
        dialogueUI.ShowDialogue(dialogue);
        for (int i = 0; i < dialogue.Dialogue.Length + 1; i++)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space));
            i++;
        }
        TransitionManager.Instance.GoToNextScene();
        yield return null;
    }
}
