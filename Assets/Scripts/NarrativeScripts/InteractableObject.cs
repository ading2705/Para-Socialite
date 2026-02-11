using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueObject DialogueObject => dialogueObject;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }
    private Renderer renderer;
    private Color originalColor;
    private Color highlightColor;

    private void Start()
    {
        Interactable = GetComponent<IInteractable>();
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        highlightColor = originalColor * 1.2f;
    }

    private void OnMouseEnter()
    {
        if (DialogueManager.Instance.IsDialogueOpen) return;
        renderer.material.color = highlightColor;
    }

    private void OnMouseExit()
    {
        if (DialogueManager.Instance.IsDialogueOpen) return;
        renderer.material.color = originalColor;
    }

    private void OnMouseDown()
    {
        if (DialogueManager.Instance.IsDialogueOpen) return;
        Interactable?.Interact(this);
        renderer.material.color = originalColor;
    }
}
