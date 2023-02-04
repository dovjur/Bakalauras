using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    public Tile[] entranceTiles;

    private int[,] map;
    List<List<Vector2Int>> caverns;
    private Vector3Int[] entrance;

    private void Start()
    {
        GenerateMap();
        DrawMap();     
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GenerateMap();
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
    }

    private void RandomMapFill()
    {
        System.Random rnd = new System.Random();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width-1 || y == 0 || y == height-1 || x == 1 || x == width-2 || y == 1 || y == height -2 || x == 2 || x == width - 3 || y == 2 || y == height - 3)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (rnd.Next(101) < fillProcentage) ? 1 : 0;
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
                int neighbourWallTiles = GetSurroundingWallCount(x,y);

                if (neighbourWallTiles > 4)
                {
                    map[x, y] = 1;
                }
                else if(neighbourWallTiles < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    private int GetSurroundingWallCount(int gridX, int gridY)
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

    private Vector3Int[] GetSurroundingTiles(int gridX, int gridY)
    {
        Vector3Int[] walls = new Vector3Int[3 * 3];
        int index = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    walls[index] = new Vector3Int(neighbourX, neighbourY, 0);
                    index++;
                }
            }
        }
        return walls;
    }
    private void IndentifyCaverns()
    {
        caverns = new List<List<Vector2Int>>();
        bool[,] visited = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 0 && !visited[x, y])
                {
                    List<Vector2Int> cavern = new List<Vector2Int>();
                    FloodFill(x, y, visited, cavern);
                    caverns.Add(cavern);
                }
            }
        }
    }

    private void FloodFill(int x, int y, bool[,] visited, List<Vector2Int> cavern)
    {
        if (x >= 0 && x < width && y >= 0 && y < height && map[x, y] == 0 && !visited[x, y])
        {
            visited[x, y] = true;
            cavern.Add(new Vector2Int(x, y));

            FloodFill(x - 1, y, visited, cavern);
            FloodFill(x + 1, y, visited, cavern);
            FloodFill(x, y - 1, visited, cavern);
            FloodFill(x, y + 1, visited, cavern);
        }
    }

    private void SetEntrance()
    {
        entrance = GetSurroundingTiles(1,1);
    }

    private void DrawMap()
    {
        RuleTile wallTile = wallTiles as RuleTile;
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
            //SetEntrance();
            //WallTilemap.SetTiles(entrance,entranceTiles);
        }
    }
}
