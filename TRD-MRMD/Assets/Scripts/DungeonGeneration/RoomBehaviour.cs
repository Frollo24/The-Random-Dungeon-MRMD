using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomBehaviour : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    private EnemyGenerator enemyGenerator;

    [Header("Room Information")]
    public RoomInfo roomInfo;
    public Transform[] wallSpawns;
    public GameObject wall;
    public Transform[] enemySpawns;
    public bool hasBeenVisited;
    public bool hasSpawned;

    [Header("Room Config")]
    public bool colourRoom;


    private void Awake()
    {
        enemyGenerator = FindObjectOfType<EnemyGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {

#if UNITY_EDITOR
        if (colourRoom)
        {
            ColourRoom();
        }
#endif

        BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ColourRoom()
    {
        foreach (var elem in GetComponentsInChildren<MeshRenderer>())
        {
            switch (roomInfo.roomType)
            {
                case RoomInfo.RoomType.Spawn:
                    elem.material.color = Color.cyan;
                    break;
                case RoomInfo.RoomType.Enemies:
                    elem.material.color = Color.magenta;
                    break;
                case RoomInfo.RoomType.Fountain:
                    elem.material.color = Color.green;
                    break;
                case RoomInfo.RoomType.Loot:
                    elem.material.color = Color.yellow;
                    break;
                case RoomInfo.RoomType.Boss:
                    elem.material.color = Color.red;
                    break;
                case RoomInfo.RoomType.NextLevel:
                    elem.material.color = Color.blue;
                    break;
            }
        }
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

    public void SetupRoom()
    {
        switch (roomInfo.roomType)
        {
            case RoomInfo.RoomType.NextLevel:
                SetupNextLevelRoom();
                break;
            case RoomInfo.RoomType.Boss:
                SetupBossRoom();
                break;
        }
    }

    void SetupNextLevelRoom()
    {
        FindObjectOfType<LevelManager>().SpawnNextLevelTrigger(this);
    }

    void SetupBossRoom()
    {
        transform.localScale = Vector3.one * 3;
    }

    public void EnterRoom()
    {
        if (hasBeenVisited) return;

        hasBeenVisited = true;

        switch (roomInfo.roomType)
        {
            case RoomInfo.RoomType.Enemies:
                EnterEnemyRoom();
                break;
            case RoomInfo.RoomType.Boss:
                EnterBossRoom();
                break;
        }
    }

    void EnterEnemyRoom()
    {
        if (!hasSpawned)
        {
            //TODO improve enemy spawning
            enemyGenerator.SpawnEnemies(enemySpawns);
        }

        hasSpawned = true;
    }

    void EnterBossRoom()
    {
        if (!hasSpawned)
        {
            //TODO spawn boss
            enemyGenerator.SpawnBoss(transform);
        }

        hasSpawned = true;
    }
}
