using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level;
    public int maxHealth;
    public int currentHealth;
    public SpriteRenderer spriteRenderer;

    public virtual void Initialize(int enemyLevel, int health)
    {
        level = enemyLevel;
        maxHealth = health;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        // Base implementation - can be empty
    }

    private void Start()
    {
        // Ensure the object is affected by physics but not by gravity
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
}   

// untuk salah nama commit wkwkwkkw