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

    public void CheckAdjacentRooms(List<Vector3> positions, Vector3 moveAmount)
    {
        if(positions.Contains(position + Vector3.forward * moveAmount.z))
        {
            adjacentRooms |= AdjacentRooms.North;
        }
        if (positions.Contains(position + Vector3.back * moveAmount.z))
        {
            adjacentRooms |= AdjacentRooms.South;
        }

        if (positions.Contains(position + Vector3.right * moveAmount.x))
        {
            adjacentRooms |= AdjacentRooms.East;
        }
        if (positions.Contains(position + Vector3.left * moveAmount.x))
        {
            adjacentRooms |= AdjacentRooms.West;
        }
    }
}
