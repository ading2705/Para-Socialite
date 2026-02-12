using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour
{
    private static WinLose _instance; //singleton
    public static WinLose Instance // public access for singleton
    {
        get{return _instance;}
    }
    [SerializeField] private string winScene; // loaded scene on win 
    [SerializeField] private string loseScene; // loaded scene on loss



    // Start is called before the first frame update
    void Start()
    {
        _instance = this; // assigns "this" obj to static instnance var n lets other scripts use wl.instance
    }
// call WinLose.Instance.Win(); or .Lose(); to use them
   public void Win()
    {// tell the manager what scene we want next n then tells it to load that scene
        TransitionManager.Instance.nextScene = winScene; 
        TransitionManager.Instance.GoToNextScene();

    }

    public void Lose()
    {
        TransitionManager.Instance.nextScene = loseScene;
        TransitionManager.Instance.GoToNextScene();
    }

}
