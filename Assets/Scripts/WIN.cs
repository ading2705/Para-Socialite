using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIN : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Yippee());
    }
    IEnumerator Yippee()
    {
        yield return new WaitForSeconds(3);
        TransitionManager.Instance.GoToNextScene();
    }
}
