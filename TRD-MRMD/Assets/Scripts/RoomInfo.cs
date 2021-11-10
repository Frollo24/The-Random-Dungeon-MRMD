using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : ScriptableObject
{
    public enum RoomType
    {
        Spawn, Enemies, Boss, Cafe, Loot
    };

    public enum AdjacentRooms
    {
        None = 0, North = 1, East = 2, South = 4, West = 8
    };

    public RoomType roomType;
    public AdjacentRooms adjacentRooms;

    public Vector3 position;
}
