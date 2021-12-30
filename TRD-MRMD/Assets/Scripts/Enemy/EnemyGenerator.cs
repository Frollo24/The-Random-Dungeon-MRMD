using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private int enemiesPerRoomAmount = 4;
    [SerializeField] private int enemiesPerRoomWeight = 10;

    public GameObject slime;
    public GameObject skeleton;
    public GameObject bat;
    public GameObject boss;

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
        int enemyWeight = 0;

        foreach (var spawn in spawnpoints)
        {
            var enemy = SelectRandomEnemy();
            enemyWeight += enemy.GetComponent<EnemyBehaviour>().coinsAmount;
            Instantiate(enemy, spawn.position, spawn.rotation);
            enemiesSpawned++;
            if (enemiesSpawned == enemiesPerRoomAmount || enemyWeight >= enemiesPerRoomWeight) break;
        }
    }

    GameObject SelectRandomEnemy()
    {
        GameObject enemy = null;

        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0:
                enemy = slime;
                break;
            case 1:
                enemy = skeleton;
                break;
            case 2:
                enemy = bat;
                break;
        }

        return enemy;
    }

    public void SpawnBoss(Transform spawnPoint)
    {
        Instantiate(boss, spawnPoint.position, spawnPoint.rotation);
    }
}
