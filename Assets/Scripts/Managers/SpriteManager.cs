using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private static SpriteManager _instance;
    public static SpriteManager Instance { get { return _instance; } }

    public Sprite defaultSprite;
    public Sprite sanitySprite;

    private void Start()
    {
        Instance = _instance;

    }

    private void

}
