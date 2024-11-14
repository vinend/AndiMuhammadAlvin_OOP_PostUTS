using UnityEngine;

public class EnemyBoss : EnemyHorizontal
{
    public EnemyBossWeapon weapon;
    private Transform player;

    public override void Initialize(int enemyLevel, int health)
    {
        base.Initialize(enemyLevel, health);
        weapon = GetComponentInChildren<EnemyBossWeapon>();
        if (weapon != null)
        {
            weapon.enabled = true;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        base.Update();
    }
}