using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPickUp : MonoBehaviour
{
    Weapon weapon;

    //This damage amount is how many times to increase damage.

    public int damageAmount;

    private void Start()
    {
        weapon = FindObjectOfType<Weapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Destroy(gameObject);
        }
    }
}
