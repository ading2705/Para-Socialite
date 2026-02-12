using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTransitionButtonBehaviour : MonoBehaviour
{
    void OnMouseDown()
    {
        TransitionManager.Instance.GoToNextScene();
    }
}
