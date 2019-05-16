using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    enum gridSpace { empty, floor, wall };
    gridSpace[,] grid;
    enum gridUnits { empty, player, enemy, chest };
    gridUnits[,] units;
    int roomHeight, roomWidth;
    const int mapSize = 40;
    Vector2 roomSizeWorldUnits = new Vector2(mapSize, mapSize);
    float worldUnitsInOneGridCell = 1;
    struct Walker
    {
        public Vector2 dir;
        public Vector2 pos;
    }
    List<Walker> walkers;
    [Range(0.0f, 1.0f)]
    public float chanceWalkerChangeDir = 0.5f;
    [Range(0.0f, 1.0f)]
    public float chanceWalkerSpawn = 0.05f;
    [Range(0.0f, 1.0f)]
    public float chanceWalkerDestroy = 0.05f;
    int maxWalkers = 10;
    [Range(0.0f, 1.0f)]
    public float percentToFill = 0.2f;
    public GameObject playerObject, portalObject;
    public GameObject[] enemyObjects, chestObjects, floorObjects, wallObjects;

    private void Start()
    {
        Setup();
        CreateFloors();
        CreateSafetyZone();
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

        units = new gridUnits[roomWidth, roomHeight];

        for (int x = 0; x < roomWidth - 1; x++)
        {
            for (int y = 0; y < roomHeight - 1; y++)
            {
                units[x, y] = gridUnits.empty;
            }
        }
        units[Mathf.RoundToInt(roomWidth / 2.0f),
            Mathf.RoundToInt(roomHeight / 2.0f)] = gridUnits.player;

        walkers = new List<Walker>();
        Walker newWalker = new Walker();
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
            foreach (Walker myWalker in walkers)
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
                    CreateUnit(walkers[i]);
                    walkers.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < chanceWalkerChangeDir)
                {
                    Walker thisWalker = walkers[i];
                    thisWalker.dir = RandomDirection();
                    walkers[i] = thisWalker;
                }
            }
            int numberChecks = walkers.Count;
            for (int i = 0; i < numberChecks; i++)
            {
                if (Random.value < chanceWalkerSpawn && walkers.Count < maxWalkers)
                {
                    Walker newWalker = new Walker();
                    newWalker.dir = RandomDirection();
                    newWalker.pos = walkers[i].pos;
                    walkers.Add(newWalker);
                }
            }



            for (int i = 0; i < walkers.Count; i++)
            {
                Walker thisWalker = walkers[i];
                thisWalker.pos += thisWalker.dir;
                thisWalker.pos.x = Mathf.Clamp(thisWalker.pos.x, 1, roomWidth - 2);
                thisWalker.pos.y = Mathf.Clamp(thisWalker.pos.y, 1, roomHeight - 2);
                walkers[i] = thisWalker;
            }
            iterations++;
        } while (iterations < 100000);

        foreach (Walker myWalker in walkers)
        {
            if (units[(int)myWalker.pos.x, (int)myWalker.pos.y] != gridUnits.player)
            {
                units[(int)myWalker.pos.x, (int)myWalker.pos.y] = gridUnits.chest;
            }

        }
    }

    private void CreateSafetyZone()
    {
        int playerPositionX = Mathf.RoundToInt(roomWidth / 2.0f);
        int playerPositionY = Mathf.RoundToInt(roomHeight / 2.0f);

        for(int x = playerPositionX-1; x<= playerPositionX+1; x++)
        {
            for (int y = playerPositionY - 1; y <= playerPositionY + 1; y++)
            {
                grid[x, y] = gridSpace.floor;
            }
        }

        for (int x = playerPositionX - 2; x <= playerPositionX + 2; x++)
        {
            for (int y = playerPositionY - 2; y <= playerPositionY + 2; y++)
            {
                if(units[x, y] != gridUnits.player)
                {
                    units[x, y] = gridUnits.empty;
                }
            }
        }

    }

    private void CreateUnit(Walker walker)
    {
        if (Random.value < 0.7f)
        {
            units[(int)walker.pos.x, (int)walker.pos.y] = gridUnits.enemy;
        }
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

    private float NumberOfEnemies()
    {
        int count = 0;
        foreach (gridUnits unit in units)
        {
            if (unit == gridUnits.enemy)
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
                        Spawn(x, y, SelectRandomObject(floorObjects));
                        break;
                    case gridSpace.wall:
                        //Debug.Log("(" + x + "," + y + "): wall");
                        Spawn(x, y, SelectRandomObject(wallObjects));
                        break;
                }

                switch (units[x,y])
                {
                    case gridUnits.empty:
                        //Debug.Log( "(" + x + "," + y + "): empty");
                        break;
                    case gridUnits.player:
                        //Debug.Log( "(" + x + "," + y + "): player");
                        //MoveObject(x, y, playerObject);
                        Spawn(x, y, portalObject);
                        break;
                    case gridUnits.enemy:
                        //Debug.Log("(" + x + "," + y + "): wall");
                        Spawn(x, y, SelectRandomObject(enemyObjects));
                        break;
                    case gridUnits.chest:
                        Spawn(x, y, SelectRandomObject(chestObjects));
                        break;
                }
            }
        }
    }

    private GameObject SelectRandomObject(GameObject[] objects)
    {
        int index = Random.Range(0, enemyObjects.Length);
        return objects[index];
    }

    private void Spawn(int x, int y, GameObject objectToSpawn)
    {
        Vector2 offset = roomSizeWorldUnits / 2.0f;
        Vector2 spawnPos = new Vector2(x, y) * worldUnitsInOneGridCell - offset;
        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

    }

    /*
    private void MoveObject(int x, int y, GameObject objectToMove)
    {
        objectToMove.transform.position = new Vector3(x+ 300.0f, y+ 500.0f, 0.0f);
    }
    */
}
