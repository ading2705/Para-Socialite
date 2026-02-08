using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    // Optional visual shown when the mouse hovers a turret in scene/game view.
    [SerializeField] private GameObject _highlight;
    // HP cost consumed when placement is confirmed.
    [SerializeField, Min(0)] private int _healthCost = 1;

    // Read by DragController during confirm.
    public int HealthCost => _healthCost;

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
}
