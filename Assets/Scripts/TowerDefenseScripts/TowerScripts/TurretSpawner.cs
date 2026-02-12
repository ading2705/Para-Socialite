using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
     [SerializeField] private GameObject _highlight;
     public GameObject towerSpawned;

      void OnMouseEnter()
    {
        // Hover feedback only; does not affect drag state.
        if (_highlight != null)
            _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        // Hide hover feedback when pointer leaves.
        if (_highlight != null)
            _highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        
        Instantiate(towerSpawned, transform.position, Quaternion.identity);
        //add on pick up SFX
    }
}
