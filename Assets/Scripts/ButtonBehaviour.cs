using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    void OnMouseDown()
    {
        StateManager.Instance.IsAliveAfterDamage(4);
        Debug.Log(StateManager.Instance.CurrentSanity());
        Debug.Log(StateManager.Instance.GetMaxSanity());
    }
}
