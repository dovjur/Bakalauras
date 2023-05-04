using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Generation")]
    [SerializeField] private int height;
    [SerializeField] private int width;

    [Range(0, 100)]
    [SerializeField] private int fillProcentage;
    [SerializeField] private int iterations;

    [Header("TileMaps & Tiles")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private ScriptableObject wallTiles;
    [SerializeField] private ScriptableObject groundTiles;
    [SerializeField] private ScriptableObject entranceTiles;

    [Header("Prefabs")]
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private GameObject barrelPrefab;

    private int[,] map;
    private int borderSize = 5;
    private Vector3Int[] entrance = new Vector3Int[3*4];
    public Vector3Int spawnPoint;

    [SerializeField] private CameraMovement mainCamera;

    public void Generate()
    {
        GenerateMap();
        DrawMap();
        SpawnObjects(30);
        mainCamera.maxPosition = new Vector2(width - borderSize - 1, height - borderSize);
    }

    private void GenerateMap()
    {
        map = new int[width, height];
        RandomMapFill();

        for (int i = 0; i < iterations; i++)
        {
            SmoothMap();
            RemoveSingleNeighbors();
        }

        CleanUp();
        RemoveSingleNeighbors();
        IndentifyCaverns();
        for (int i = 0; i < 10; i++)
        {
            RemoveSingleNeighbors();
        }
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
                int neighbourWallTiles = GetSurroundingWallTileCount(x,y,1,1);

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

    private int GetSurroundingWallTileCount(int gridX, int gridY, int offsetX, int offsetY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - offsetX; neighbourX <= gridX + offsetX; neighbourX++)
        {
            for (int neighbourY = gridY - offsetY; neighbourY <= gridY + offsetY; neighbourY++)
            {
                if (IsInMapRange(neighbourX,neighbourY))
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
        List<Vector3Int> possibleEntrances = new List<Vector3Int>();
        for (int x = borderSize; x < width - borderSize; x++)
        {
            for (int y = borderSize; y < height - borderSize; y++)
            {
                if (CanBeEntrance(x,y))
                {
                    possibleEntrances.Add(new Vector3Int(x,y));
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
                if (IsInMapRange(neighbourX,neighbourY))
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
                if (IsInMapRange(neighbourX,neighbourY))
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
                    if (cavernRegion.Count < 50)
                    {
                        foreach (Vector2Int tile in cavernRegion)
                        {
                            map[tile.x, tile.y] = 1;
                        }
                    }
                    else
                    {
                        caverns.Add(new Cavern(cavernRegion, map));
                    }
                }
            }
        }
        if (caverns.Count > 4)
        {
            ConnectCaverns(caverns);
        }
        else
        {
            GenerateMap();
        }
    }

    private void ConnectCaverns(List<Cavern> allCaverns)
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
    
    private void FloodFill(int gridX, int gridY, bool[,] visited, List<Vector2Int> cavernRegion)
    {
        if (IsInMapRange(gridX,gridY) && map[gridX, gridY] == 0 && !visited[gridX, gridY])
        {
            visited[gridX, gridY] = true;
            cavernRegion.Add(new Vector2Int(gridX, gridY));

            FloodFill(gridX - 1, gridY, visited, cavernRegion);
            FloodFill(gridX + 1, gridY, visited, cavernRegion);
            FloodFill(gridX, gridY - 1, visited, cavernRegion);
            FloodFill(gridX, gridY + 1, visited, cavernRegion);
        }
    }

    public bool IsInMapRange(int gridX, int gridY)
    {
        return gridX >= 0 && gridX < width && gridY >= 0 && gridY < height;
    }

    private void RemoveSingleNeighbors()
    {
        int sum = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 1 && x - 1 > 0 && x + 1 < width && y - 1 > 0 && y + 1 < height)
                {
                    sum = map[x - 1, y] + map[x + 1, y] + map[x, y - 1] + map[x, y + 1];
                    if (sum <= 1)
                    {
                        map[x, y] = 0;
                    }
                    if (map[x, y] == 1 && GetVerticalNeighbors(x, y) == 1)
                    {
                        map[x, y - 1] = 0;
                        map[x, y + 1] = 0;
                    }
                }
            }
        }
    }
    private void CleanUp()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x-1 > 0 && x +1 <width && y-1 >0 && y+1 <height)
                {               
                    if (map[x, y] == 1 && map[x, y - 1] == 0)
                    {
                        ThickenWalls(x, y);
                    }
                }
            }
        }
    }
    private int GetVerticalNeighbors(int gridX, int gridY)
    {
        int sum = 0;
        for (int y = gridY-2; y <= gridY+2; y++)
        {
            if (IsInMapRange(gridX, y) && y != gridY)
            {
                sum += map[gridX, y];
            }
        }
        return sum;
    }
    private void ThickenWalls(int gridX, int gridY)
    {
        for (int y = 1; y <= 4; y++)
        {
            if (gridY + y < height)
            {
                if (map[gridX, gridY + y] == 0)
                {
                    map[gridX, gridY + y] = 1;
                }
            }
        }
    }

    public GameObject SpawnLootChest()
    {
        Vector3 lootCord = new Vector3();
        float maxDistanceToLoot = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x,y] == 0)
                {
                    int neighbourWallTiles = GetSurroundingWallTileCount(x, y,1,1);
                    float distance = (Mathf.Pow(x - spawnPoint.x, 2) + Mathf.Pow(y - spawnPoint.y, 2));
                    if (distance > maxDistanceToLoot && neighbourWallTiles == 0)
                    {
                        maxDistanceToLoot = distance;
                        lootCord = new Vector3(x, y, 0);
                    }
                }
            }
        }
        GameObject chest = Instantiate(chestPrefab, lootCord, Quaternion.identity);
        return chest;
    }

    private void SpawnObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 position = GetRandomGroundTile();
            Instantiate(barrelPrefab,position,Quaternion.identity);
        }
    }

    private void DrawMap()
    {
        RuleTile wallTile = wallTiles as RuleTile;
        RuleTile entranceTile = entranceTiles as RuleTile;
        RuleTile groundTile = groundTiles as RuleTile;

        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x,y] == 1)
                    {
                        wallTilemap.SetTile(new Vector3Int(x,y), wallTile);
                    }
                    groundTilemap.SetTile(new Vector3Int(x, y), groundTile);
                }
            }
            for (int i = 0; i < entrance.Count(); i++)
            {
                wallTilemap.SetTile(entrance[i], entranceTile);
            }
        }
    }
    public int[,] GetMap()
    {
        return map;
    }

    public Vector2 GetRandomGroundTile()
    {
        int rngX = UnityEngine.Random.Range(0, width);
        int rngY = UnityEngine.Random.Range(0, height);
        if (map[rngX,rngY] == 0)
        {
            return new Vector2(rngX,rngY);
        }
        else
        {
            return GetRandomGroundTile();
        }
    }
}
