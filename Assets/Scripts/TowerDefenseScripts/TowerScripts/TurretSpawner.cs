using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// I had to mess with this to fix some logic errors swapping from the old system to the new one, so debrief mel and annie on that
public class TurretSpawner : MonoBehaviour
{
     [SerializeField] private GameObject _highlight;
     public GameObject towerSpawned;

     [SerializeField] private GridManager _gridManager; // new

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

    void OnMouseDown() // this is different
    {
        // mels original code:
        //Instantiate(towerSpawned, transform.position, Quaternion.identity);
        //add on pick up SFX

        Vector3Int cell = _gridManager.GetCellUnderMouse(); // gettin da cell under the mouse

        // only placing tower if not path and not occupied
        if (!_gridManager.IsPath(cell) && !_gridManager.IsOccupied(cell))
        {
            Vector3 worldPos = _gridManager.GetCellCenter(cell);

            Instantiate(towerSpawned, worldPos, Quaternion.identity);

            _gridManager.SetOccupied(cell);
        }

    }
}
