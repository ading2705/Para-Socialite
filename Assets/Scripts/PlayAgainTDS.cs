using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainTDS : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("CompleteTDSceneWithAllFeatures");
    }
}
