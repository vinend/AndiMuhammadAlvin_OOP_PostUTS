using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    private void Start()
    {
        // Find all enemy spawners in the scene if not assigned
        if (enemySpawners == null || enemySpawners.Length == 0)
        {
            enemySpawners = FindObjectsOfType<EnemySpawner>();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        CountTotalEnemies();
    }

    public void StartNewWave()
    {
        waveNumber++;
        timer = 0;
        
        // Increase difficulty for each spawner
        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.maxEnemies += waveNumber;
                spawner.spawnCountMultiplier++;
                spawner.totalKillWave = 0;
                spawner.requiredKillsForBoss += 5; // Increase kills needed for boss each wave
            }
        }
    }

    private void CountTotalEnemies()
    {
        totalEnemies = 0;
        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner != null)
            {
                totalEnemies += spawner.totalKill;
            }
        }
    }
}

// untuk salah nama commit wkwkwkkw
