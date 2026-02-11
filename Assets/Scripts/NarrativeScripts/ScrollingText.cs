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
        textLabel.text = string.Empty;
        float t = 0;
        int charIndex = 0;

        while (charIndex < line.Length)
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, line.Length);

            textLabel.text = line.Substring(0, charIndex);
            yield return null;
        }

        textLabel.text = line;
    }
}
