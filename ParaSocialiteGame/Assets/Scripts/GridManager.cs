using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
  [SerializeField] private int _width, _height;
  [SerializeField] private Tile _PlacementTile, _PathTile;


  [SerializeField] private Transform _cam;

  void Start()
  {
    GenerateGrid();
  }

  void GenerateGrid()
  {
    for (int x = 0; x < _width; x++)
    {
      for (int y = 0; y < _height; y++)
      {
        var randomTile = Random.Range(0, 6) == 3 ? _PathTile : _PlacementTile;
        var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
        spawnedTile.name = $"Tile {x} {y}";
        spawnedTile.Init(x, y);
      }
    }
    _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
  }
}
