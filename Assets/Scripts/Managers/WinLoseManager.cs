using UnityEngine;

public class WinLoseManager : MonoBehaviour
{
    private static WinLoseManager _instance; //singleton
    public static WinLoseManager Instance {get{return _instance;}}

    [SerializeField] private string winScene; // win
    [SerializeField] private string loseScene; // loss

    void Start()
    {
        _instance = this;
    }

    public void Win()
    // WinLoseManager.Instance.Win(); <-------------------------- we gotta add this to somewhere, like if enemy = 0 
    // or smth cuz idk where that is
    {
        TransitionManager.Instance.nextScene = winScene;
        TransitionManager.Instance.GoToNextScene();
    }

    public void Lose()
    {
        TransitionManager.Instance.nextScene = loseScene;
        TransitionManager.Instance.GoToNextScene();
    }
}