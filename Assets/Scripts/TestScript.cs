using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] public ConfirmationWindow myConfirmationWindow;

    void Start()
    {
        OpenConfirmationWindow("Are you sure?");
    }

    private void OpenConfirmationWindow(string message)
    {
        myConfirmationWindow.gameObject.SetActive(true);
        myConfirmationWindow.yesButton.onClick.AddListener(YesClicked);
        myConfirmationWindow.noButton.onClick.AddListener(NoClicked);
    }

    private void YesClicked()
    {
        myConfirmationWindow.gameObject.SetActive(false);
        
    }

     private void NoClicked()
    {
        myConfirmationWindow.gameObject.SetActive(false);
        
    }
}
