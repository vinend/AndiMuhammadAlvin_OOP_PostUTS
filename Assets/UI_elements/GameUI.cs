using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("UI Text References")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI enemyCountText;

    private HealthComponent playerHealth;
    private EnemySpawner enemySpawner;
    private int points = 0;

    private void Start()
    {
        // Get references
        playerHealth = Player.Instance.GetComponent<HealthComponent>();
        enemySpawner = FindObjectOfType<EnemySpawner>();

        // Subscribe to the OnEnemyDestroyed event
        HealthComponent.OnEnemyDestroyed += HandleEnemyDestroyed;

        // Initial UI update
        UpdateAllUI();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnEnemyDestroyed event
        HealthComponent.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }

    private void Update()
    {
        UpdateAllUI();
    }

    private void UpdateAllUI()
    {
        UpdateHealthUI();
        UpdatePointsUI();
        UpdateWaveUI();
        UpdateEnemyCountUI();
    }

    private void UpdateHealthUI()
    {
        if (playerHealth != null)
        {
            healthText.text = $"Health: {playerHealth.GetHealth()}";
        }
    }

    private void UpdatePointsUI()
    {
        pointsText.text = $"Points: {points}";
    }

    private void UpdateWaveUI()
    {
        if (enemySpawner != null)
        {
            waveText.text = $"Wave: {enemySpawner.combatManager.waveNumber}";
        }
    }

    private void UpdateEnemyCountUI()
    {
        if (enemySpawner != null)
        {
            enemyCountText.text = $"Enemies: {enemySpawner.totalKill}";
        }
    }

    private void HandleEnemyDestroyed()
    {
        // Update points when an enemy is destroyed
        points += 3; // Assuming each enemy gives 1 point, adjust as needed
        UpdatePointsUI();
    }
}