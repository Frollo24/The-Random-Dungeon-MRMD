using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBehaviour : EnemyBehaviour
{
    public bool IsDying = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);
        }
    }
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
