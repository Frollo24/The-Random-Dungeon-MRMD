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

    public GameObject TRBL_Room;

    [SerializeField] private Vector3 currentPos = Vector3.zero;

    [SerializeField] private List<RoomInfo> roomInfoList; //TODO save useful room info for rearrangement
    [SerializeField] private List<Vector3> positions;

    // Start is called before the first frame update
    void Start()
    {
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
        int rand = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(RoomInfo.RoomType)).Length - 2);
        switch (rand)
        {
            case 0:
                roomInfo.roomType = RoomInfo.RoomType.Enemies;
                break;
            case 1:
                roomInfo.roomType = RoomInfo.RoomType.Cafe;
                break;
            case 2:
                roomInfo.roomType = RoomInfo.RoomType.Loot;
                break;
        }
    }

    void RearrangeLevel()
    {
        foreach (var roomInfo in roomInfoList)
        {
            var room = Resources.Load("RoomPlaceholder") as GameObject;
            /** //TODO change path and add RoomBehaviour script
            var resource = Resources.Load<RoomBehaviour>("path");
            var room = (resource as RoomBehaviour).gameObject;
            //*/

            Instantiate(room, roomInfo.position, Quaternion.identity);
            //var room = Instantiate(TRBL_Room, roomInfo.position, Quaternion.identity);
        }
    }
}
