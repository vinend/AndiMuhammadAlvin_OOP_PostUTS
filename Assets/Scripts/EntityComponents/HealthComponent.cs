using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int maxHealth;
    private int health;

    public delegate void EnemyDestroyedHandler();
    public static event EnemyDestroyedHandler OnEnemyDestroyed;

    private void Start()
    {
        health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public void Subtract(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Current health: {health}");
        
        if (health <= 0)
        {
            Debug.Log($"{gameObject.name} has been destroyed!");
            Destroy(gameObject);
            if (gameObject.tag == "Enemy")
            {
                OnEnemyDestroyed?.Invoke();
            }
        }
    }
}

