using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyHorizontalPrefab;
    public GameObject enemyForwardPrefab;
    public GameObject enemyTargetingPrefab;
    public GameObject enemyBossPrefab;
    public int enemyLevel = 1;
    public int enemyHealth = 100;
    public int rows = 3;
    public int columns = 5;
    public float horizontalSpacing = 2f;
    public float verticalSpacing = 2f;
    public float spawnInterval = 1f;
    public int maxEnemies = 10;
    public int respawnThreshold = 5;

    private int currentEnemyCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float startX = mainCamera.transform.position.x - cameraWidth / 2;
        float startY = mainCamera.transform.position.y + cameraHeight / 2 - 1f; // Adjust spawn height as needed

        while (true)
        {
            currentEnemyCount = GameObject.FindObjectsOfType<EnemyHorizontal>().Length +
                                GameObject.FindObjectsOfType<EnemyForward>().Length +
                                GameObject.FindObjectsOfType<EnemyTargeting>().Length +
                                GameObject.FindObjectsOfType<EnemyBoss>().Length;

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < columns; col++)
                    {
                        if (currentEnemyCount >= maxEnemies)
                            break;

                        Vector2 spawnPosition = new Vector2(startX + col * horizontalSpacing, startY - row * verticalSpacing);
                        GameObject enemy = Instantiate(enemyHorizontalPrefab, spawnPosition, Quaternion.identity);
                        EnemyHorizontal enemyHorizontal = enemy.GetComponent<EnemyHorizontal>();
                        enemyHorizontal.Initialize(enemyLevel, enemyHealth);
                        currentEnemyCount++;
                        yield return new WaitForSeconds(spawnInterval);
                    }
                }

                for (int row = 0; row < rows; row++) 
                {
                    GameObject enemyForward = Instantiate(enemyForwardPrefab);
                    EnemyForward enemyForwardScript = enemyForward.GetComponent<EnemyForward>();
                    enemyForwardScript.Initialize(enemyLevel, enemyHealth);
                    currentEnemyCount++;
                    yield return new WaitForSeconds(spawnInterval);
                }

                    GameObject enemyTargeting = Instantiate(enemyTargetingPrefab);
                    EnemyTargeting enemyTargetingScript = enemyTargeting.GetComponent<EnemyTargeting>();
                    enemyTargetingScript.Initialize(enemyLevel, enemyHealth);
                    currentEnemyCount++;
                    yield return new WaitForSeconds(spawnInterval);
                
                    GameObject enemyBoss = Instantiate(enemyBossPrefab);
                    EnemyBoss enemyBossScript = enemyBoss.GetComponent<EnemyBoss>();
                    enemyBossScript.Initialize(enemyLevel, enemyHealth);
                    currentEnemyCount++;
                    yield return new WaitForSeconds(spawnInterval);
                
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void OnEnemyDestroyed()
    {
        int currentEnemyCount = GameObject.FindObjectsOfType<EnemyHorizontal>().Length +
                                GameObject.FindObjectsOfType<EnemyForward>().Length +
                                GameObject.FindObjectsOfType<EnemyTargeting>().Length +
                                GameObject.FindObjectsOfType<EnemyBoss>().Length;

        if (currentEnemyCount <= respawnThreshold)
        {
            StartCoroutine(SpawnEnemies());
        }
    }
}