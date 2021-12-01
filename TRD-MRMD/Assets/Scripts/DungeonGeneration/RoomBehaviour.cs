using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomBehaviour : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    [Header("Room Information")]
    public RoomInfo roomInfo;
    public Transform[] wallSpawns;
    public GameObject wall;
    public Transform[] enemySpawns;
    public bool hasBeenVisited;
    public bool hasSpawned;


    // Start is called before the first frame update
    void Start()
    {
        BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWalls()
    {
        if ((roomInfo.adjacentRooms & RoomInfo.AdjacentRooms.North) == 0)
        {
            Instantiate(wall, wallSpawns[0]);
        }
        if ((roomInfo.adjacentRooms & RoomInfo.AdjacentRooms.South) == 0)
        {
            Instantiate(wall, wallSpawns[1]);
        }

        if ((roomInfo.adjacentRooms & RoomInfo.AdjacentRooms.East) == 0)
        {
            Instantiate(wall, wallSpawns[2]);
        }
        if ((roomInfo.adjacentRooms & RoomInfo.AdjacentRooms.West) == 0)
        {
            Instantiate(wall, wallSpawns[3]);
        }
    }

    public void BuildNavMesh()
    {
        navMeshSurface.collectObjects = CollectObjects.All;
        navMeshSurface.defaultArea = 1;
        // navMeshSurface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
        navMeshSurface.ignoreNavMeshAgent = false;
        navMeshSurface.BuildNavMesh();

    }

    public void EnterRoom()
    {
        if (!hasBeenVisited) return;

        hasBeenVisited = true;
    }
}
