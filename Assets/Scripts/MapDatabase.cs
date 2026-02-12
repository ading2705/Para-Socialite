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
                // enemy path
                new Vector2Int(0,4),
                new Vector2Int(1,4),
                new Vector2Int(2,4),
                new Vector2Int(3,4),
                new Vector2Int(4,4),
                new Vector2Int(5,4),
                new Vector2Int(5,5),
                new Vector2Int(5,6),
                new Vector2Int(5,7),
                new Vector2Int(6,7),
                new Vector2Int(7,7),
                new Vector2Int(8,7),
                new Vector2Int(9,7),
                new Vector2Int(10,7),
                new Vector2Int(11,7),
                new Vector2Int(12,7),
                new Vector2Int(12,6),
                new Vector2Int(12,5),
                new Vector2Int(12,4),
                new Vector2Int(12,3),
                new Vector2Int(12,2),
                new Vector2Int(12,1),
                new Vector2Int(12,0),

                // obsticles
                new Vector2Int(1,1),
                new Vector2Int(1,2),
                new Vector2Int(0,2),
                new Vector2Int(0,3),


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
            }
        )
    };
}
