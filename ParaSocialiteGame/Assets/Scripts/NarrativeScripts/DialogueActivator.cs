using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{

    public void Interact(InteractableObject interactable)
    {
        interactable.DialogueUI.ShowDialogue(interactable.DialogueObject);
    }

    private void OnMouseDown()
    {

    }
}
