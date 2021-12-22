using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBehaviour : EnemyBehaviour
{
    public bool IsDying = true;

    private void OnApplicationQuit()
    {
        IsDying = false;
    }

    private void OnDisable()
    {
        if(IsDying)
            FindObjectOfType<LevelManager>().SpawnEndGameTrigger(transform);
    }
}
