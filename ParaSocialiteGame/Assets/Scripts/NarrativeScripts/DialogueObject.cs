using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    public string[] Dialogue => dialogue; // prvents any code from writing to the Dialogue
    public Response[] Responses => responses;
    public bool HasResponses => Responses != null && Responses.Length > 0;


}
