using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour : MonoBehaviour
{
    [SerializeField] TextAsset script;
    Renderer m_Renderer;
    Color m_OriginalColour;
    Color m_MouseOverColour = new Color(0.8f, 0.5f, 0.2f, 0.5f);
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_OriginalColour = m_Renderer.material.color;
    }

    void OnMouseOver()
    {
        Debug.Log("hi");
        m_Renderer.material.color = m_MouseOverColour;
    }

    void OnMouseExit()
    {
        m_Renderer.material.color = m_OriginalColour;
    }

    void OnMouseDown()
    {

    }
}
