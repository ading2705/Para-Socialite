using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class TransitionManager : MonoBehaviour
{
    private static TransitionManager _instance;
    public static TransitionManager Instance { get { return _instance; } }
    public string[] nextScenes;

    private void Start()
    {
        _instance = this;
    }
    public void GoToNextScene()
    {
        Assert.IsNotNull(nextScenes[0]);
        SceneManager.LoadScene(nextScenes[0]);
    }

    public void GoToNextScene(string scene)
    {
        if (System.Array.IndexOf(nextScenes, scene) != -1)
        {
            SceneManager.LoadScene(scene);
        }
        else
        {
            Debug.Log("Loaded incorrect scene using string overload!");
        }
    }

    public void GoToNextScene(int index)
    {
        Assert.IsNotNull(nextScenes[index]);
        SceneManager.LoadScene(nextScenes[index]);
    }
}
