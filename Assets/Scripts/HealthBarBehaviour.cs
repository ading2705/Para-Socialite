using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBehaviour : MonoBehaviour
{
    public int MaxWidth;
    public int Height;

    // Update is called once per frame
    void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(MaxWidth, Height);
    }
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(StateManager.Instance.CurrentSanity() * MaxWidth / StateManager.Instance.GetMaxSanity(), Height);
    }
}
