using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    public List<Vector2Int> pathTiles;
    public List<Vector2Int> placementTiles;

    public MapData(List<Vector2Int> path, List<Vector2Int> placement)
    {
        pathTiles = path;
        placementTiles = placement;
    }
}
