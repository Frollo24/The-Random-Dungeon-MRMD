using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBehaviour : EnemyBehaviour
{
    private void OnDestroy()
    {
        var room = FindObjectOfType<RoomBehaviour>();
        FindObjectOfType<LevelManager>().SpawnNextLevelTrigger(room);
    }
}
