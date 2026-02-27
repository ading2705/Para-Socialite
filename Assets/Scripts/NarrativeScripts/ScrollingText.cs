using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This controls the typewriter effect for dialogue
public class ScrollingText : MonoBehaviour
{
    [SerializeField] private float typeSpeed = 50f; // control speed of letters appearing
    public Coroutine Run(string line, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(line, textLabel));
    }

    public IEnumerator TypeText(string line, TMP_Text textLabel)
    {
        AudioController.Instance.StartBlip();
        textLabel.text = string.Empty;
        float t = 0;
        int charIndex = 0;
        bool hasColour = line.Contains("<color=");
        int startIndex = hasColour ? 15 : 0;
        while (charIndex < line.Length - startIndex)
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, line.Length - startIndex);
            textLabel.text = line.Substring(0, charIndex + startIndex);
            yield return null;
        }

        textLabel.text = line;
        AudioController.Instance.StopBlip();
    }
}
