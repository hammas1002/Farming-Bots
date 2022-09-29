using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    Player_Movement player;
    public int healAmount;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
