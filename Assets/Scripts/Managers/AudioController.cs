using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;
    public static AudioController Instance { get { return _instance; } }
    public AudioSource themeSource;
    public AudioSource effectSource;

    public AudioClip towerTheme, narrativeTheme;
    public AudioClip towerSelect, towerPlace;

    // Dictionaries for easier use
    public Dictionary<string, AudioClip> themes = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> effects = new Dictionary<string, AudioClip>();

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        themes["tower"] = towerTheme;
        themes["narrative"] = narrativeTheme;
        effects["select"] = towerSelect;
        effects["place"] = towerPlace;
    }

    // // Update is called once per frame
    void Update()
    {
    }

    // Plays a sound effect given a name 
    // Note: At the moment the sound effect MUST be initalized within this script and added to the dictionary
    public void PlayEffect(string name)
    {
        effectSource.PlayOneShot(effects[name]);
    }

    // Plays a looping theme song given a name 
    // Note: At the moment the theme MUST be initalized within this script and added to the dictionary

    public void PlayTheme(string name)
    {
        // stop current audio
        themeSource.Stop();
        themeSource.clip = themes[name];
        themeSource.Play();
        themeSource.loop = true;

    }
}
