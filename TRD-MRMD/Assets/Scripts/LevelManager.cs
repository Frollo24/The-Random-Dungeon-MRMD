using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject nextLevelTrigger;

    public void SpawnNextLevelTrigger(RoomBehaviour room)
    {
        Instantiate(nextLevelTrigger, room.transform.position + Vector3.up, room.transform.rotation);
    }

    public void SpawnEndGameTrigger(Transform boss)
    {
        Instantiate(nextLevelTrigger, boss.position, boss.rotation);
    }
}
