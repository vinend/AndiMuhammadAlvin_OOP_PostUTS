using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject enemyHorizontalPrefab;
    public GameObject enemyForwardPrefab;
    public GameObject enemyTargetingPrefab;
    public GameObject enemyBossPrefab; // Assign boss prefab in inspector

    [Header("Spawn Settings")]
    public float spawnInterval = 0.4f;
    public int maxEnemies = 20;
    public int respawnThreshold = 5;
    public GameObject spawnedEnemy;
    
    [Header("Spawn Control")]
    public int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    public int totalKillWave = 0;
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;
    public int requiredKillsForBoss = 10;
    public float bossSpawnDelay = 2f;

    [Header("References")]
    public CombatManager combatManager;
    public bool isSpawning = false;
    private bool isBossSpawned = false;
    private bool isBossDefeated = false;

    private void Start()
    {
        spawnCount = defaultSpawnCount;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        isSpawning = true;

        while (true)
        {
            if (!isBossSpawned && totalKillWave >= requiredKillsForBoss)
            {
                yield return new WaitForSeconds(bossSpawnDelay);
                SpawnBoss();
                isBossSpawned = true;
            }
            else if (!isBossSpawned && CanSpawn())
            {
                for (int i = 0; i < spawnCount * spawnCountMultiplier; i++)
                {
                    SpawnRandomEnemy();
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnBoss()
    {
        if (enemyBossPrefab != null)
        {
            Vector3 spawnPos = CalculateSpawnPosition();
            GameObject boss = Instantiate(enemyBossPrefab, spawnPos, Quaternion.identity);
            EnemyBoss bossComponent = boss.GetComponent<EnemyBoss>();
            if (bossComponent != null)
            {
                bossComponent.Initialize(combatManager.waveNumber, maxEnemies * 2);
            }
        }
    }

    private void SpawnRandomEnemy()
    {
        if (maxEnemies <= 0) return;

        // Calculate spawn position at top of screen
        Vector3 spawnPos = CalculateSpawnPosition();

        // Randomly select enemy type
        int enemyType = Random.Range(0, 3);
        GameObject enemyPrefab = null;

        switch (enemyType)
        {
            case 0:
                enemyPrefab = enemyHorizontalPrefab;
                break;
            case 1:
                enemyPrefab = enemyForwardPrefab;
                break;
            case 2:
                enemyPrefab = enemyTargetingPrefab;
                break;
        }

        if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.Initialize(combatManager.waveNumber, maxEnemies);
            }
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        
        float startX = mainCamera.transform.position.x - cameraWidth / 2;
        float startY = mainCamera.transform.position.y + cameraHeight / 2;
        
        float randomX = startX + Random.Range(0, cameraWidth);
        return new Vector3(randomX, startY, 0);
    }

    private bool CanSpawn()
    {
        int currentEnemyCount = FindObjectsOfType<Enemy>().Length;
        return currentEnemyCount < maxEnemies;
    }

    private void CheckSpawnCountIncrease()
    {
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            spawnCount++;
            totalKillWave = 0;
        }
    }

    private void OnEnable()
    {
        HealthComponent.OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void OnDisable()
    {
        HealthComponent.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }

    private void HandleEnemyDestroyed()
    {
        totalKill++;
        totalKillWave++;

        // Check if the destroyed enemy was a boss
        if (isBossSpawned && FindObjectsOfType<EnemyBoss>().Length == 0)
        {
            isBossDefeated = true;
            isBossSpawned = false;
            totalKillWave = 0;
            combatManager.StartNewWave();
        }
    }

    public void OnBossDefeated()
    {
        isBossDefeated = true;
        isBossSpawned = false;
        totalKillWave = 0;
        
        // Increase difficulty for next wave
        requiredKillsForBoss += 5;
        spawnCountMultiplier++;
        
        if (combatManager != null)
        {
            combatManager.StartNewWave();
        }
    }
}