using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    private HealthComponent healthComponent;
    private InvincibilityComponent invincibilityComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        invincibilityComponent = GetComponent<InvincibilityComponent>();
    }

    public void Damage(int amount)
    {
        if (invincibilityComponent != null && invincibilityComponent.isInvincible)
            return;

        healthComponent?.Subtract(amount);
        invincibilityComponent?.StartInvincibility();
    }

    // Overloaded Damage method to apply damage from a Bullet
    public void Damage(Bullet bullet)
    {
        if (healthComponent != null)
        {
            healthComponent.Subtract(bullet.damage);
            Debug.Log($"{gameObject.name} received {bullet.damage} damage from a bullet.");
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} does not have a HealthComponent!");
        }
    }
}
