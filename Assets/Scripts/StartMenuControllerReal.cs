using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuControllerReal : MonoBehaviour
{
    public void OnStartClick()
    {
        TransitionManager.Instance.GoToNextScene();
    }

}
