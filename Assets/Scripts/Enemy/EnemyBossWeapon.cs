using UnityEngine;
using UnityEngine.Pool;

public class EnemyBossWeapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootIntervalInSeconds = 1f;

    private IObjectPool<EnemyBossBullet> objectPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;

    private void Awake()
    {
        // Initialize the object pool
        objectPool = new ObjectPool<EnemyBossBullet>(
            createFunc: CreateBullet,
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck: collectionCheck,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0f;
        }
    }

    public void Shoot()
    {
        if (bulletSpawnPoint != null && objectPool != null)
        {
            EnemyBossBullet pooledBullet = objectPool.Get();
            pooledBullet.transform.position = bulletSpawnPoint.position;
            pooledBullet.transform.rotation = bulletSpawnPoint.rotation;
        }
    }

    private EnemyBossBullet CreateBullet()
    {
        GameObject bulletObject = Instantiate(bulletPrefab);
        EnemyBossBullet bullet = bulletObject.GetComponent<EnemyBossBullet>();
        bullet.ObjectPool = objectPool;
        return bullet;
    }

    private void OnGetFromPool(EnemyBossBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(EnemyBossBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(EnemyBossBullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}