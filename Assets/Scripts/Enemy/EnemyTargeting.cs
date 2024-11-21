using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 3f;
    public float rotationSpeed = 200f; // Speed of rotation towards the player
    private Transform player;

    public override void Initialize(int enemyLevel, int health)
    {
        base.Initialize(enemyLevel, health);
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetSpawnPosition();
    }

    private void SetSpawnPosition()
    {
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Camera.main.aspect;
        float spawnOffset = 2f; // Distance beyond screen edge
        
        // Select random side (0=top, 1=right, 2=bottom, 3=left)
        int side = Random.Range(0, 4);
        
        float spawnX, spawnY;
        
        switch (side)
        {
            case 0: // Top
                spawnX = Random.Range(-screenWidth/2, screenWidth/2);
                spawnY = screenHeight/2 + spawnOffset;
                break;
            case 1: // Right
                spawnX = screenWidth/2 + spawnOffset;
                spawnY = Random.Range(-screenHeight/2, screenHeight/2);
                break;
            case 2: // Bottom
                spawnX = Random.Range(-screenWidth/2, screenWidth/2);
                spawnY = -(screenHeight/2 + spawnOffset);
                break;
            default: // Left
                spawnX = -(screenWidth/2 + spawnOffset);
                spawnY = Random.Range(-screenHeight/2, screenHeight/2);
                break;
        }
        
        transform.position = new Vector2(spawnX, spawnY);
    }

    private void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); 
        }
    }
}
