using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon;

    void Awake()
    {
        weapon = Instantiate(weaponHolder);
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("OnTriggerEnter2D called with: " + other.name); 

    if (other.CompareTag("Player"))
    {
        Debug.Log("Collided with Player");
        Player player = other.GetComponent<Player>();
        if (player != null)
        {   
            Debug.Log("Player has touched the weapon!");
            if (player.currentWeapon != null)
            {
                player.currentWeapon.transform.SetParent(null);
                player.currentWeapon.gameObject.SetActive(false); 
            }
            player.PickUpWeapon(weapon);
            AttachWeaponToPlayer(player);
            TurnVisual(true);
        }
    }
}

    void AttachWeaponToPlayer(Player player)
    {
        weapon.transform.SetParent(player.transform, false);
        weapon.gameObject.SetActive(true); 

        SpriteRenderer weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();
        if (weaponSpriteRenderer != null)
        {
            weaponSpriteRenderer.enabled = true;
        }
    }

    void TurnVisual(bool on)
    {
        foreach (var component in weapon.GetComponents<Component>())
        {
            TurnVisual(on, weapon);
        }
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        weapon.GetComponent<SpriteRenderer>().enabled = on;
        weapon.GetComponent<Animator>().enabled = on;
    }
}