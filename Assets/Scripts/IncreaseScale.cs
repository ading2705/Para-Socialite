using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseScale : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private float targetingRange = 5f;
    private int level = 1;
    // Start is called before the first frame update
    private float targetingRangeBase;

    void Start()
    {
        targetingRangeBase = targetingRange;
        upgradeButton.onClick.AddListener(Upgrade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade()
    {
        transform.localScale = new Vector3((targetingRangeBase * Mathf.Pow(level, 0.4f)), (targetingRangeBase * Mathf.Pow(level, 0.4f)), (targetingRangeBase * Mathf.Pow(level, 0.4f)));
        level++;
    }

}
