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
        float spawnX = Random.Range(-screenWidth, screenWidth);
        float spawnY = Random.Range(-screenHeight, screenHeight);
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


