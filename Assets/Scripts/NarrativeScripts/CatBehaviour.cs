using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour : MonoBehaviour
{
    [SerializeField] GameObject hand;
    Vector3 handStart;
    private void Start()
    {
        hand.SetActive(false);
        handStart = hand.transform.position;
    }

    private void OnMouseDown()
    {
        StartCoroutine(Pet());
    }

    private IEnumerator Pet()
    {
        if (hand.activeSelf) yield break;
        hand.SetActive(true);
        for (int i = 0; i < 24; i++)
        {
            hand.transform.position += new Vector3(0.1f, -0.05f, 0);
            yield return new WaitForSeconds(0.04f);
        }
        hand.SetActive(false);
        hand.transform.position = handStart;
    }
}
