using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private int enemiesPerRoomAmount = 4;

    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies(Transform[] spawnpoints)
    {
        int enemiesSpawned = 0;

        foreach (var spawn in spawnpoints)
        {
            Instantiate(enemy, spawn.position, spawn.rotation);
            enemiesSpawned++;
            if (enemiesSpawned == enemiesPerRoomAmount) break;
        }
    }
}
