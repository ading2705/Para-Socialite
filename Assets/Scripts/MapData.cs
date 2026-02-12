using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    public List<Vector2Int> pathTiles;
    
    public MapData(List<Vector2Int> path)
    {
        pathTiles = path;
        
    }
}