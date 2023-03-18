using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage
{
    public Vector2Int startTile;
    public Vector2Int endTile;
    public int distance;
    
    public Passage() { }
    public Passage(Vector2Int startTile, Vector2Int endTile, int distance)
    {
        this.startTile = startTile;
        this.endTile = endTile;
        this.distance = distance;
    }
}
