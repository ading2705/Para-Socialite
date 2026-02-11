using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour : MonoBehaviour
{
    [SerializeField] GameObject hand;
    private void Start(){
        hand.SetActive(false);
    }

    private void OnMouseDown(){
        StartCoroutine(Pet());
    }

    private IEnumerator Pet(){
        hand.SetActive(true);
        yield return new WaitForSeconds(1);
        hand.SetActive(false);
    }
}
