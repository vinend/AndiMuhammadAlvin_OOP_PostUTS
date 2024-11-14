using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBossBullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<EnemyBossBullet> objectPool;

    public IObjectPool<EnemyBossBullet> ObjectPool
    {
        get => objectPool;
        set => objectPool = value;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Set velocity when bullet is enabled from pool
        rb.velocity = Vector2.down * bulletSpeed; // Shoot vertically down
        StartCoroutine(DeactivateRoutine(2f)); // 2 second lifetime
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (objectPool != null)
        {
            objectPool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthComponent>().Subtract(damage);
            ReturnToPool();
        }
        else if (!other.CompareTag("Enemy"))
        {
            ReturnToPool();
        }
    }
}
