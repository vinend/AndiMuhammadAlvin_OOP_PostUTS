using UnityEngine;

public class EnemyBoss : Enemy 
{
    [Header("Boss Components")]
    public EnemyBossWeapon weapon;
    private Transform player;

    [Header("Boss Stats")]
    public float moveSpeed = 3f;
    public float healthMultiplier = 2f;
    public float verticalPosition = 3f; // Fixed Y position above player
    private float direction = 1f; // 1 for right, -1 for left
    private float leftBound;
    private float rightBound;
    private Camera mainCamera;
    
    public override void Initialize(int enemyLevel, int health)
    {
        // Multiply boss health to make it tougher
        base.Initialize(enemyLevel, (int)(health * healthMultiplier));
        
        // Initialize weapon
        weapon = GetComponentInChildren<EnemyBossWeapon>();
        if (weapon != null)
        {
            weapon.enabled = true;
        }
        
        // Find player reference
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        mainCamera = Camera.main;
        SetupBoundaries();
        SetInitialPosition();
    }

    private void SetupBoundaries()
    {
        // Get screen boundaries with some padding
        Vector3 leftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0.1f, 0, 0));
        Vector3 rightEdge = mainCamera.ViewportToWorldPoint(new Vector3(0.9f, 0, 0));
        leftBound = leftEdge.x;
        rightBound = rightEdge.x;
    }

    private void SetInitialPosition()
    {
        // Spawn off right side of screen at vertical center
        float screenCenterY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y;
        Vector3 spawnPos = new Vector3(
            mainCamera.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x, 
            screenCenterY,
            0
        );
        transform.position = spawnPos;
    }

    private void Update()
    {
        // Move horizontally
        transform.position += new Vector3(moveSpeed * direction * Time.deltaTime, 0, 0);

        // Check bounds and reverse direction
        if (transform.position.x >= rightBound)
        {
            direction = -1f;
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= leftBound)
        {
            direction = 1f;
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.OnBossDefeated();
        }
    }
}


// untuk salah nama commit wkwkwkkw