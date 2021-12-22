using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBehaviour : EnemyBehaviour
{
    private void OnDisable()
    {
        FindObjectOfType<LevelManager>().SpawnEndGameTrigger(transform);
    }
}
