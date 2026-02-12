using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScriptManager : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite sanitySprite;
    private static SpriteRenderer renderer;
    private bool damaging = true;
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        ChangeSprite(damaging);
    }

    public void ChangeSprite(bool hasSanity)
    {
        renderer.sprite = hasSanity ? defaultSprite : sanitySprite;
    }

    // for testing
    void OnMouseDown()
    {
        ChangeSprite(damaging);
        damaging = !damaging;
    }
}
