using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{

    [Header ("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> objectPool;

    public IObjectPool<Bullet> ObjectPool
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
        rb.velocity = transform.up * bulletSpeed;
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
}
