using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioObject")]
public class AudioObject : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip audio;
    public string Name => name;
    public AudioClip Audio => audio;
}
