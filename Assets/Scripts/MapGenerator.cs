using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public int height;
    public int width;

    [Range(0, 100)]
    public int fillProcentage;

    public int iterations;

    public Tilemap GroundTilemap;
    public Tilemap WallTilemap;
    public ScriptableObject wallTiles;
    public Tile groundTile;
    public ScriptableObject entranceTiles;
    public GameObject playerPrefab;

    private int[,] map;
    private int borderSize = 5;
    private Vector3Int[] entrance = new Vector3Int[3 * 4];
    private Vector2Int spawnPoint;
    

    private void Start()
    {
        GenerateMap();
        DrawMap();     
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GroundTilemap.ClearAllTiles();
            WallTilemap.ClearAllTiles();
            CleanUp();
            DrawMap();
        }
    }

    private void GenerateMap()
    {
        map = new int[width, height];
        RandomMapFill();

        for (int i = 0; i < iterations; i++)
        {
            SmoothMap();
        }

        IndentifyCaverns();
        SetEntrance();
    }

    private void RandomMapFill()
    {
        System.Random rng = new System.Random();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x < borderSize || x > width - borderSize || y < borderSize || y > height - borderSize)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (rng.Next(101) < fillProcentage) ? 1 : 0;
                } 
            }
        }
    }

    private void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallTileCount(x,y);

                if (neighbourWallTiles > 4)
                {
                    map[x, y] = 1;
                }
                else if (neighbourWallTiles < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    private int GetSurroundingWallTileCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (gridX != neighbourX || gridY != neighbourY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }

    private void SetEntrance()
    {
        List<Vector2Int> possibleEntrances = new List<Vector2Int>();
        for (int x = borderSize; x < width - borderSize; x++)
        {
            for (int y = borderSize; y < height - borderSize; y++)
            {
                if (CanBeEntrance(x,y))
                {
                    possibleEntrances.Add(new Vector2Int(x,y));
                }
            }
        }

        if (possibleEntrances.Count == 0)
        {
            GenerateMap();
        }

        System.Random rng = new System.Random();
        int selected = rng.Next(0, possibleEntrances.Count);

        spawnPoint = possibleEntrances[selected];
        GetEntranceTiles(possibleEntrances[selected].x, possibleEntrances[selected].y);
    }

    private bool CanBeEntrance(int gridX, int gridY)
    {
        bool canBeEntrance = true;
        for (int neighbourX = gridX - 2; neighbourX <= gridX + 2; neighbourX++)
        {
            for (int neighbourY = gridY; neighbourY <= gridY + 2; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (map[neighbourX, gridY - 1] != 0 || map[neighbourX, neighbourY] != 1)
                    {
                        canBeEntrance = false;
                    }
                }       
            }
        }
        return canBeEntrance;
    }

    private void GetEntranceTiles(int gridX, int gridY)
    {
        int index = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY; neighbourY <= gridY + 2; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    entrance[index] = new Vector3Int(neighbourX, neighbourY, 0);
                    index++;
                }
            }
        }
    }
    private void IndentifyCaverns()
    {
        List<Cavern> caverns = new List<Cavern>();
        bool[,] visited = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 0 && !visited[x, y])
                {
                    List<Vector2Int> cavernRegion = new List<Vector2Int>();
                    FloodFill(x, y, visited, cavernRegion);
                    caverns.Add(new Cavern(cavernRegion, map));
                    //if (cavernRegion.Count < 50)
                    //{
                    //    foreach (Vector2Int tile in cavernRegion)
                    //    {
                    //        //map[tile.x, tile.y] = 1;
                    //    }
                    //}
                    //else
                    //{
                    //    caverns.Add(new Cavern(cavernRegion, map));
                    //}
                }
            }
        }
        if (caverns.Count > 1)
        {
            Test(caverns);
        }
    }

    private void Test(List<Cavern> allCaverns)
    {
        Dictionary<Tuple<Cavern, Cavern>, Passage> passages = new Dictionary<Tuple<Cavern, Cavern>, Passage>();
        for (int i = 0; i < allCaverns.Count; i++)
        {
            for (int j = i + 1; j < allCaverns.Count; j++)
            {
                Passage passage = FindPassage(allCaverns[i], allCaverns[j]);
                passages.Add(Tuple.Create(allCaverns[i], allCaverns[j]), passage);
                passages.Add(Tuple.Create(allCaverns[j], allCaverns[i]), passage);
            }
        }

        List<Cavern> visitedCaverns = new List<Cavern>();
        allCaverns.Sort();
        visitedCaverns.Add(allCaverns[0]);

        List<Passage> connectingPassages = new List<Passage>();

        while (visitedCaverns.Count < allCaverns.Count)
        {
            Cavern nextCavern = null;
            Passage shortestPassage = null;
            int shortestDistance = int.MaxValue;

            foreach (Cavern visitedCavern in visitedCaverns)
            {
                foreach (Cavern cavern in allCaverns)
                {
                    if (!visitedCaverns.Contains(cavern))
                    {
                        Tuple<Cavern, Cavern> key = Tuple.Create(visitedCavern, cavern);
                        Passage passage = passages[key];
                        int distance = passage.distance;

                        if (distance < shortestDistance)
                        {
                            shortestDistance = distance;
                            nextCavern = cavern;
                            shortestPassage = passage;
                        }
                    }
                }
            }
            connectingPassages.Add(shortestPassage);
            
            visitedCaverns.Add(nextCavern);
        }

        foreach (Passage passage in connectingPassages)
        {
            CreatePassage(passage.startTile,passage.endTile);
        }
    }

    private Passage FindPassage(Cavern cavernA, Cavern cavernB)
    {
        int shortestDistance = int.MaxValue;
        Vector2Int closestTileA = new Vector2Int();
        Vector2Int closestTileB = new Vector2Int();

        for (int indexA = 0; indexA < cavernA.edgeTiles.Count; indexA++)
        {
            for (int indexB = 0; indexB < cavernB.edgeTiles.Count; indexB++)
            {
                Vector2Int tileA = cavernA.edgeTiles[indexA];
                Vector2Int tileB = cavernB.edgeTiles[indexB];
                int distance = (int)(Mathf.Pow(tileA.x - tileB.x, 2) + Mathf.Pow(tileA.y - tileB.y, 2));
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestTileA = tileA;
                    closestTileB = tileB;
                }
            }
        }
        return new Passage(closestTileA, closestTileB, shortestDistance);
    }

    private void CreatePassage(Vector2Int tileA, Vector2Int tileB)
    {
        List<Vector2Int> line = GetLine(tileA,tileB);
        foreach (Vector2Int coord in line)
        {
            DrawCircle(coord,3);
        }
    }

    private void DrawCircle(Vector2Int coord, int r)
    {
        for (int x = -r; x <= r; x++)
        {
            for (int y = -r; y <= r; y++)
            {
                if (x*x + y*y <= r*r)
                {
                    int drawX = coord.x + x;
                    int drawY = coord.y + y;
                    if (IsInMapRange(drawX,drawY))
                    {
                        map[drawX, drawY] = 0;
                    }
                }
            }
        }
    }

    private List<Vector2Int> GetLine(Vector2Int from, Vector2Int to)
    {
        List<Vector2Int> line = new List<Vector2Int>();

        int x = from.x;
        int y = from.y;
        int dx = to.x - from.x;
        int dy = to.y - from.y;
        bool inverted = false;
        int step = Math.Sign(dx);
        int gradientStep = Math.Sign(dy);
        int longest = Math.Abs(dx);
        int shortest = Math.Abs(dy);

        if (longest < shortest)
        {
            inverted = true;
            longest = Math.Abs(dy);
            shortest = Math.Abs(dx);
            step = Math.Sign(dy);
            gradientStep = Math.Sign(dx);
        }
        int gradientAccumuliation = longest / 2;
        for (int i = 0; i < longest; i++)
        {
            line.Add(new Vector2Int(x,y));
            if (inverted)
            {
                y += step;
            }
            else
            {
                x += step;
            }
            gradientAccumuliation += shortest;
            if (gradientAccumuliation >= longest)
            {
                if (inverted)
                {
                    x += gradientStep;
                }
                else
                {
                    y += gradientStep;
                }
            }
        }
        return line;
    }
    
    private void FloodFill(int x, int y, bool[,] visited, List<Vector2Int> cavernRegion)
    {
        if (x >= 0 && x < width && y >= 0 && y < height && map[x, y] == 0 && !visited[x, y])
        {
            visited[x, y] = true;
            cavernRegion.Add(new Vector2Int(x, y));

            FloodFill(x - 1, y, visited, cavernRegion);
            FloodFill(x + 1, y, visited, cavernRegion);
            FloodFill(x, y - 1, visited, cavernRegion);
            FloodFill(x, y + 1, visited, cavernRegion);
        }
    }

    bool IsInMapRange(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    private void CleanUp()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x,y] == 1 && x-1 >0 && y-1 >0 && x+1<width && y+1 < height)
                {
                    if (map[x-1,y] + map[x+1,y] + map[x,y-1] + map[x,y-1] <1)
                    {
                        map[x, y] = 0;
                    }
                }
            }
        }
    }

    private void DrawMap()
    {
        RuleTile wallTile = wallTiles as RuleTile;
        RuleTile entranceTile = entranceTiles as RuleTile;
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x,y] == 1)
                    {
                        WallTilemap.SetTile(new Vector3Int(x,y), wallTile);
                    }
                    GroundTilemap.SetTile(new Vector3Int(x, y), groundTile);
                }
            }
            for (int i = 0; i < entrance.Count(); i++)
            {
                WallTilemap.SetTile(entrance[i], entranceTile);
            }
        }
        //Instantiate(playerPrefab,new Vector3(spawnPoint.x,spawnPoint.y,0),Quaternion.identity);
    }
}
