using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;
    public static AudioController Instance { get { return _instance; } }
    public AudioSource themeSource;
    public AudioSource effectSource;
    public AudioSource dialogueBlipSource;

    public AudioObject[] themes;
    public AudioObject[] effects;
    public AudioObject[] blips;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
    }
    // Plays a sound effect given a name 
    public void PlayEffect(string name)
    {
        foreach (AudioObject obj in effects)
        {
            if (obj.Name == name) { effectSource.PlayOneShot(obj.Audio); return; }
        }
        Debug.LogError("The audio object with name " + name + "does not exist in the effects array.");
    }

    // Plays a looping theme song given the sanity level
    public void PlayTheme(int sanity)
    {
        if (sanity >= themes.Length)
        {
            Debug.LogError("Invalid sanity level called.");
        }
        // stop current audio
        themeSource.Stop();
        themeSource.clip = themes[sanity].Audio;
        themeSource.Play();
        themeSource.loop = true;
    }
}
