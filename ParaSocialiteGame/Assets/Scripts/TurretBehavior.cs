using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;



    void OnMouseEnter()
    {
        _highlight.SetActive(true);
        AudioController.Instance.PlayEffect("select");
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
        AudioController.Instance.PlayEffect("place");

    }
}
