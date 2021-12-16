using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneratorManager : MonoBehaviour
{
    public enum RandomRoomNumberMethod
    {
        Fixed = 0, Random = 1
    };

    public RandomRoomNumberMethod GenerationMethod; //Generates a fixed number of rooms or a random number of rooms

    public int FixedValue = 8;

    //Bounds of the random method
    public int RandomLowerBound = 5;
    public int RandomUpperBound = 10;

    public Vector3 MoveAmount = new Vector3(10, 0, 10); //Distance between rooms

    [Header("Spawning Room Config")]
    [SerializeField] private uint fountainRoomsSpawned;
    [SerializeField] private uint maxFountainRooms = 2;
    [SerializeField] private uint lootRoomsSpawned;
    [SerializeField] private uint maxLootRooms = 2;

    [Header("Private serialized stuff")]
    [SerializeField] private Vector3 currentPos = Vector3.zero;

    [SerializeField] private List<RoomInfo> roomInfoList; //TODO save useful room info for rearrangement
    [SerializeField] private List<Vector3> positions;

    // Start is called before the first frame update
    void Start()
    {
        fountainRoomsSpawned = 0;
        lootRoomsSpawned = 0;

        positions = new List<Vector3>();
        roomInfoList = new List<RoomInfo>();
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateLevel()
    {
        switch (GenerationMethod)
        {
            case RandomRoomNumberMethod.Fixed:
                GenerateProcLevel(FixedValue);
                break;
            case RandomRoomNumberMethod.Random:
                int random = UnityEngine.Random.Range(RandomLowerBound, RandomUpperBound + 1);
                GenerateProcLevel(random);
                break;
        }

        RearrangeLevel();
    }

    void GenerateProcLevel(int num)
    {
        for (int i = 0; i < num;)
        {
            int rand = UnityEngine.Random.Range(0, 4);

            if (!positions.Contains(currentPos))
            {
                var roomInfo = ScriptableObject.CreateInstance<RoomInfo>();
                if (i == 0)
                {
                    roomInfo.roomType = RoomInfo.RoomType.Spawn;
                }
                else if (i == num - 1)
                {
                    roomInfo.roomType = RoomInfo.RoomType.Boss;
                }
                else
                    SetRandomRoom(roomInfo);

                roomInfo.position = currentPos;
                roomInfoList.Add(roomInfo);
                positions.Add(currentPos);

                i++;
            }

            switch (rand)
            {
                case 0:
                    currentPos += Vector3.forward * MoveAmount.z;
                    break;
                case 1:
                    currentPos += Vector3.back * MoveAmount.z;
                    break;
                case 2:
                    currentPos += Vector3.left * MoveAmount.x;
                    break;
                case 3:
                    currentPos += Vector3.right * MoveAmount.x;
                    break;
            }
        }
    }

    void SetRandomRoom(RoomInfo roomInfo)
    {
        float rnd = UnityEngine.Random.Range(0.0f, 1.0f);

        if (rnd < 0.1f && fountainRoomsSpawned < maxFountainRooms)
        {
            roomInfo.roomType = RoomInfo.RoomType.Fountain;
            fountainRoomsSpawned++;
        }
        else if (rnd < 0.4f && lootRoomsSpawned < maxLootRooms)
        {
            roomInfo.roomType = RoomInfo.RoomType.Loot;
            lootRoomsSpawned++;
        }
        else if (rnd < 0.6f)
        {
            roomInfo.roomType = RoomInfo.RoomType.Enemies;
        }
        else
        {
            roomInfo.roomType = RoomInfo.RoomType.Empty;
        }
    }

    void RearrangeLevel()
    {
        foreach (var roomInfo in roomInfoList)
        {
            roomInfo.CheckAdjacentRooms(positions, MoveAmount);

            //TODO change path and add RoomBehaviour script
            var resource = Resources.Load<RoomBehaviour>("RoomPlaceholder");
            //var room = (resource as RoomBehaviour).gameObject;

            var room = Instantiate(resource, roomInfo.position, Quaternion.identity);
            room.roomInfo = roomInfo;
            room.SpawnWalls();
            //room.BuildNavMesh();
            Debug.Log("Instantiated room: " + roomInfo.roomType);
        }
    }
}
