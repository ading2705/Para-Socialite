using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeSectionIndicator : MonoBehaviour
{
    // so this is just sorta here to be somewhere to put the music :D
    void Start()
    {
        AudioController.Instance.PlayTheme("narrative");
    }
}
