using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
  [SerializeField] private int _width, _height;
  [SerializeField] private Tile _PlacementTile, _PathTile;
  [SerializeField] private Transform _cam;

// choosing a map
[SerializeField] private int _selectedMapIndex = 0;

  private HashSet<Vector2Int> _pathSet;
  private HashSet<Vector2Int> _placementSet;

  // maps for levels r in map database

  void Start(){
    // added a safety jus in case
    _selectedMapIndex = Mathf.Clamp(_selectedMapIndex, 0, MapDatabase.Maps.Count - 1);

    MapData map = MapDatabase.Maps[_selectedMapIndex];
    _pathSet = new HashSet<Vector2Int>(map.pathTiles);
    _placementSet = new HashSet<Vector2Int>(map.placementTiles);
    
    GenerateGrid();

  }

  void GenerateGrid()
  {
    for (int x = 0; x < _width; x++)
    {
      for (int y = 0; y < _height; y++)
      {
        Vector2Int coord = new Vector2Int(x,y);

        Tile tileToSpawn = null;
        
        if (_pathSet.Contains(coord))
        tileToSpawn = _PathTile;
        else if (_placementSet.Contains(coord))
        tileToSpawn = _PlacementTile;
        else
        continue; // skips empty tiles 

        var spawnedTile = Instantiate(tileToSpawn, new Vector3(x,y,0), Quaternion.identity);

        spawnedTile.name = $"Tile {x} {y}";
        spawnedTile.Init(x,y);

      }
    }

    _cam.transform.position = new Vector3((float)_width/ 2 - 0.5f, (float)_height/ 2 - 0.5f, -10);
  }
}
 /*
  void GenerateGrid(){ 
     for(int x = 0; x < _width; x++ ){
      for(int y = 0; y < _height; y++){
         var randomTile = Random.Range(0,6) == 3 ? _PathTile : _PlacementTile;
         var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
         spawnedTile.name = $"Tile {x} {y}";
         spawnedTile.Init(x,y);
      }
    }
    _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
  }
}
*/