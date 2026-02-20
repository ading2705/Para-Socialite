using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    public int MaxWidth;
    public int Height;
    [SerializeField] private Color fullColor;
    [SerializeField] private Color emptyColor;

    // For some reason you need to have StateManager in Canvas not Admin IDK why will fix later
    void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(MaxWidth, Height);
        GetComponent<Image>().color = fullColor;
    }
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(StateManager.Instance.CurrentSanity() * MaxWidth / StateManager.Instance.GetMaxSanity(), Height);
        if (StateManager.Instance.CurrentSanity() > 0)
        {
            GetComponent<Image>().color = emptyColor + (fullColor - emptyColor) * ((float)StateManager.Instance.CurrentSanity() / (float)StateManager.Instance.GetMaxSanity());
        }
    }
}
