using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeSectionIndicator : MonoBehaviour
{
    [SerializeField] GameObject transitionFade;
    // so this is just sorta here to be somewhere to put the music :D
    void Start()
    {
        // AudioController.Instance.PlayTheme("narrative");
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        transitionFade.SetActive(true);
        yield return new WaitForSeconds(2);
        transitionFade.SetActive(false);
    }
}
