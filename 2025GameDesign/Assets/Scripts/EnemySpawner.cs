using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Enemy Setup
    public GameObject enemyPrefab;
    public int maxEnemies = 5;
    public float spawnInterval = 3f;

    //Spawn Area
    public bool useSpawnPoints = true;
    public Transform[] spawnPoints;

    public float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            TrySpawnEnemy();
            timer = 0f;
        }
    }

    void TrySpawnEnemy()
    {
        if (CountEnemies() >=maxEnemies)
        {
            return;
        }

        Vector3 spawnPos = Vector3.zero;

        if(useSpawnPoints && spawnPoints.Length > 0)
        {
            spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        }

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    int CountEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Ghoul").Length;
    }

    private void OnDrawGizmosSelected()
    {
        if(!useSpawnPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(10f, 0f, 10f));
        }
    }
}
