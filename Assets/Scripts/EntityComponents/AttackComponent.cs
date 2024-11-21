using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] public Bullet bullet;
    [SerializeField] public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the same tag
        if (other.gameObject.tag == gameObject.tag)
        {
            return;
        }

        // Attempt to get the HitboxComponent from the collided object
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {   

            // If the bullet is not null, use it to damage the target, otherwise use direct damage
            if (bullet != null)
            {
                hitbox.Damage(bullet);
                Debug.Log($"Bullet hit {other.name}, applying bullet damage: {bullet.damage}");
            }
            else
            {
                hitbox.Damage(damage);
                Debug.Log($"Direct hit on {other.name}, applying direct damage: {damage}");
            }

            // Destroy the bullet after hitting the target
            
            if(this.gameObject.tag == "Bullet")
                {
                    Destroy(this.gameObject);
                }

            InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();
            if (invincibility != null)
            {
                invincibility.StartInvincibility();
            }


        }
        else
        {
            Debug.LogWarning($"{other.name} does not have a HitboxComponent!");
        }

    }
}




