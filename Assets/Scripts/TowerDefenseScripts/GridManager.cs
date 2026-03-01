using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
  [SerializeField] private int _width, _height;
  [SerializeField] private Tilemap _placementTilemap, _pathTilemap;
  [SerializeField] private Camera _cam; // can't use transform 
  [SerializeField] private TileBase _occupiedTile;

  // new and improved grid manager :>
  public Vector3Int GetCellUnderMouse()
  {
    Vector3 worldPos = _cam.ScreenToWorldPoint(Input.mousePosition);
    return _placementTilemap.WorldToCell(worldPos);
  }
  public bool IsPath(Vector3Int cell)
    {
        return _pathTilemap.HasTile(cell);
    }
  public bool IsOccupied(Vector3Int cell) // new version of placement/path
  {
    return _placementTilemap.HasTile(cell);
  }
  public void PlaceTower(Vector3Int cell, TileBase towerTile)
  {
    if (!IsPath(cell))
    {
      _placementTilemap.SetTile(cell, towerTile);
    }
  }

  public Vector3Int GetMouseCell()
  {
    Vector3 worldPos = _cam.ScreenToWorldPoint(Input.mousePosition);
    return _placementTilemap.WorldToCell(worldPos);
  }
  public Vector3 GetCellCenter(Vector3Int cell)
  {
    return _placementTilemap.GetCellCenterWorld(cell);
  }
  
  public void SetOccupied(Vector3Int cell)
  {
    _placementTilemap.SetTile(cell, _occupiedTile);
  }

}