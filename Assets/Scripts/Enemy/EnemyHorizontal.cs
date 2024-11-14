using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 5f;
    private Vector2 direction;

    public override void Initialize(int enemyLevel, int health)
    {
        base.Initialize(enemyLevel, health);
        SetRandomSpawnPosition();
    }

    private void SetRandomSpawnPosition()
    {
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float spawnX = Random.value > 0.5f ? screenWidth : -screenWidth;
        transform.position = new Vector2(spawnX, Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize));
        direction = spawnX > 0 ? Vector2.left : Vector2.right;
    }

    public void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Destroy the enemy if it goes offscreen
        if (Mathf.Abs(transform.position.x) > Camera.main.orthographicSize * Camera.main.aspect + 1)
        {
            Destroy(gameObject);
        }
    }
}
