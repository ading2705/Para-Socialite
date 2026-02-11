using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextParser : MonoBehaviour
{
    private static TextParser _instance;
    public static TextParser Instance { get { return _instance; } }
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Taken from lab 5 BarkSystem script
    public List<string> ParseText(TextAsset script)
    {
        List<string> dialogue = new List<string>();
        string[] lines = script.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            dialogue.Add(lines[i]);
        }
        return dialogue;
    }
}
