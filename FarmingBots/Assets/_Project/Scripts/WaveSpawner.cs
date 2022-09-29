using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBtwSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBtwWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool finishedSpawning;

    public GameObject boss;
    public Transform bossSpawnPoints;

    public GameObject healthBar;

    private int extraHealthAmount;

    private void Start()
    {
        extraHealthAmount = 0;
        player = FindObjectOfType<Player_Movement>().transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBtwWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];
        for (int i =0;i<currentWave.count;i++)
        {
            if (player==null)
            {
                yield break;
            }
            Enemy randomEnemy = currentWave.enemies[UnityEngine.Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            Enemy enemyUfo = Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation) as Enemy;

            //increasing enemy health with each waves restart.
            enemyUfo.SetHealth(extraHealthAmount);

            if (i==currentWave.count-1)
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }

            yield return new WaitForSeconds(currentWave.timeBtwSpawns);
        }
    }
    private void Update()
    {
        if (finishedSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length==0)
        {
            finishedSpawning = false;
            if (currentWaveIndex+1<waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                Instantiate(boss, bossSpawnPoints.position, bossSpawnPoints.rotation);
                healthBar.SetActive(true);
            }
        }
    }

    public void RestartWaves()
    {
        currentWaveIndex = -1;
        finishedSpawning = true;
        extraHealthAmount += 4;
    }

    public int GetCurrentWaveIndex()
    {
        return currentWaveIndex;
    }
}
