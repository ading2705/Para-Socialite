using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the narrative scene
public class NarrativeSectionIndicator : MonoBehaviour
{
    private static NarrativeSectionIndicator _instance;
    public static NarrativeSectionIndicator Instance { get { return _instance; } }
    [SerializeField] GameObject transitionFade;
    void Start()
    {
        _instance = this;
        StartCoroutine(FadeIn());
        AudioController.Instance.PlayTheme(0);
    }

    private IEnumerator FadeIn()
    {
        transitionFade.SetActive(true);
        yield return new WaitForSeconds(2);
        transitionFade.SetActive(false);
    }
}
