using System.Collections.Generic;
using UnityEngine;


// PLEASE ADJUST THESEEEEE 
public static class MapDatabase
{
    public static List<MapData> Maps = new List<MapData>()
    {
        // lvl 1 
        new MapData(
            new List<Vector2Int>() // path tiles
            {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(2,0),
                new Vector2Int(2,1),
                new Vector2Int(2,2),
            },

            new List<Vector2Int>() // placement tiles
            {
                new Vector2Int(0,1),
                new Vector2Int(1,1),
                new Vector2Int(3,2),
            }
        ),

        // lvl 2
        new MapData(
            new List<Vector2Int>() // path tiles
            {
                new Vector2Int(0,0),
                new Vector2Int(0,1),
                new Vector2Int(0,2),
                new Vector2Int(1,2),
            },

            new List<Vector2Int>() //placement tiles
            {
                new Vector2Int(1,0),
                new Vector2Int(2,1),
            }
        )
    };
}
