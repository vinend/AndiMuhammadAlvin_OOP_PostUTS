using UnityEngine;

public class EnemyForward : Enemy
{
    public float speed = 5f;
    private Vector2 direction = Vector2.down;

    public override void Initialize(int enemyLevel, int health)
    {
        base.Initialize(enemyLevel, health);
        SetSpawnPosition();
    }

    private void SetSpawnPosition()
    {
        float screenHeight = Camera.main.orthographicSize * 2;
        float spawnY = Camera.main.transform.position.y + screenHeight / 2;
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float spawnX = Random.Range(-screenWidth, screenWidth);
        transform.position = new Vector2(spawnX, spawnY);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Destroy the enemy if it goes offscreen
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 1)
        {
            Destroy(gameObject);
        }
    }
}
