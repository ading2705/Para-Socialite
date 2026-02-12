using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private static TransitionManager _instance;
    public static TransitionManager Instance { get { return _instance; } }
    public string nextScene;

    private void Start()
    {
        _instance = this;
    }
    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
