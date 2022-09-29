using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weaponToEquip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
                collision.collider.GetComponent<Player_Movement>().ChangeWeapon(weaponToEquip);
            Destroy(gameObject);
        }
    }
}
