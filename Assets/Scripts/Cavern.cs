using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavern: IComparable<Cavern>
{
    public List<Vector2Int> tiles;
    public List<Vector2Int> edgeTiles;
    public List<Cavern> connectedCaverns;
    public int size;

    public Cavern() { }
    public Cavern(List<Vector2Int> cavernTiles, int[,] map)
    {
        tiles = cavernTiles;
        size = cavernTiles.Count;
        connectedCaverns = new List<Cavern>();

        edgeTiles = new List<Vector2Int>();
        foreach (var tile in tiles)
        {
            for (int x = tile.x-1; x <= tile.x+1; x++)
            {
                for (int y = tile.y-1; y <= tile.y+1; y++)
                {
                    if (tile.x == x || tile.y == y)
                    {
                        if (map[x,y] == 1)
                        {
                            edgeTiles.Add(tile);
                        }
                    }
                }
            }
        }
    }

    public int CompareTo(Cavern otherCavern)
    {
        return otherCavern.size.CompareTo(size);
    }
}
