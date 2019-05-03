using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    enum gridSpace { empty, floor, wall };
    gridSpace[,] grid;
    int roomHeight, roomWidth;
    Vector2 roomSizeWorldUnits = new Vector2(30, 30);
    float worldUnitsInOneGridCell = 1;
    struct walker
    {
        public Vector2 dir;
        public Vector2 pos;
    }
    List<walker> walkers;
    float chanceWalkerChangeDir = 0.5f;
    float chanceWalkerSpawn = 0.05f;
    float chanceWalkerDestroy = 0.05f;
    int maxWalkers = 10;
    float percentToFill = 0.2f;
    public GameObject wallObject, floorObject;

    private void Start()
    {
        Setup();
        CreateFloors();
        CreateWalls();
        SpawnLevel();
    }

    private void Setup()
    {
        roomHeight = Mathf.RoundToInt(roomSizeWorldUnits.y / worldUnitsInOneGridCell);
        roomWidth = Mathf.RoundToInt(roomSizeWorldUnits.x / worldUnitsInOneGridCell);

        grid = new gridSpace[roomWidth, roomHeight];

        for (int x = 0; x < roomWidth - 1; x++)
        {
            for (int y = 0; y < roomHeight - 1; y++)
            {
                grid[x, y] = gridSpace.empty;
            }
        }

        walkers = new List<walker>();
        walker newWalker = new walker();
        newWalker.dir = RandomDirection();
        newWalker.pos = new Vector2(Mathf.RoundToInt(roomWidth / 2.0f),
            Mathf.RoundToInt(roomHeight / 2.0f));
        walkers.Add(newWalker);
    }

    private Vector2 RandomDirection()
    {
        int choice = Mathf.FloorToInt(Random.value * 4.0f);
        switch (choice)
        {
            case 0:
                return Vector2.up;
            case 1:
                return Vector2.right;
            case 2:
                return Vector2.down;
            default:
                return Vector2.left;
        }
    }

    private void CreateFloors()
    {
        int iterations = 0;
        do
        {
            foreach (walker myWalker in walkers)
            {
                grid[(int)myWalker.pos.x, (int)myWalker.pos.y] = gridSpace.floor;
            }
            if ((float)NumberOfFloors() / (float)grid.Length > percentToFill)
            {
                break;
            }


            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < chanceWalkerDestroy && walkers.Count > 1)
                {
                    walkers.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < chanceWalkerChangeDir)
                {
                    walker thisWalker = walkers[i];
                    thisWalker.dir = RandomDirection();
                    walkers[i] = thisWalker;
                }
            }
            int numberChecks = walkers.Count;
            for (int i = 0; i < numberChecks; i++)
            {
                if (Random.value < chanceWalkerSpawn && walkers.Count < maxWalkers)
                {
                    walker newWalker = new walker();
                    newWalker.dir = RandomDirection();
                    newWalker.pos = walkers[i].pos;
                    walkers.Add(newWalker);
                }
            }
            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < chanceWalkerChangeDir)
                {
                    walker thisWalker = walkers[i];
                    thisWalker.pos += thisWalker.dir;
                    walkers[i] = thisWalker;
                }
            }
            for (int i = 0; i < walkers.Count; i++)
            {
                walker thisWalker = walkers[i];
                thisWalker.pos.x = Mathf.Clamp(thisWalker.pos.x, 1, roomWidth - 2);
                thisWalker.pos.y = Mathf.Clamp(thisWalker.pos.y, 1, roomHeight - 2);
                walkers[i] = thisWalker;
            }
            iterations++;
        } while (iterations < 100000);


    }

    private float NumberOfFloors()
    {
        int count = 0;
        foreach (gridSpace space in grid)
        {
            if (space == gridSpace.floor)
                count++;
        }
        return count;
    }

    private void CreateWalls()
    {
        for (int x = 1; x < roomWidth - 1; x++)
        {
            for (int y = 1; y < roomHeight - 1; y++)
            {
                if (grid[x, y] == gridSpace.floor)
                {
                    if (grid[x, y + 1] == gridSpace.empty)
                    {
                        grid[x, y + 1] = gridSpace.wall;
                    }
                    if (grid[x, y - 1] == gridSpace.empty)
                    {
                        grid[x, y - 1] = gridSpace.wall;
                    }
                    if (grid[x + 1, y] == gridSpace.empty)
                    {
                        grid[x + 1, y] = gridSpace.wall;
                    }
                    if (grid[x - 1, y] == gridSpace.empty)
                    {
                        grid[x - 1, y] = gridSpace.wall;
                    }
                }
            }
        }
    }

    private void SpawnLevel()
    {
        for (int x = 0; x < roomWidth; x++)
        {
            for (int y = 0; y < roomHeight; y++)
            {

                switch (grid[x, y])
                {
                    case gridSpace.empty:
                        //Debug.Log( "(" + x + "," + y + "): empty");
                        break;
                    case gridSpace.floor:
                        //Debug.Log("(" + x + "," + y + "): floor");
                        Spawn(x, y, floorObject);
                        break;
                    case gridSpace.wall:
                        //Debug.Log("(" + x + "," + y + "): wall");
                        Spawn(x, y, wallObject);
                        break;
                }
            }
        }
    }

    private void Spawn(int x, int y, GameObject objectToSpawn)
    {
        Vector2 offset = roomSizeWorldUnits / 2.0f;
        Vector2 spawnPos = new Vector2(x, y) * worldUnitsInOneGridCell - offset;
        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

    }
}
