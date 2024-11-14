using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField]    private float shootIntervalInSeconds = 0.5f; // Reduced interval for more frequent shots

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    private bool isEquipped = false; // Set to false by default

    private void Awake()
    {
        // Initialize the object pool
        objectPool = new ObjectPool<Bullet>(
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
        if (!isEquipped) return;

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
            Bullet pooledBullet = objectPool.Get();
            pooledBullet.transform.position = bulletSpawnPoint.position;
            pooledBullet.transform.rotation = bulletSpawnPoint.rotation;
            
        }
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bullet);
        bulletInstance.ObjectPool = objectPool;
        return bulletInstance;
    }

    private void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    // Method to be called when player picks up the weapon
    public void OnEquip()
    {
        isEquipped = true;
        timer = shootIntervalInSeconds; // Allow immediate first shot
        Debug.Log($"Weapon {gameObject.name} equipped"); // Add this line
    }

    // Method to be called when player drops/unequips the weapon
    public void OnUnequip()
    {
        isEquipped = false;
        Debug.Log($"Weapon {gameObject.name} equipped"); // Add this line
    }
}