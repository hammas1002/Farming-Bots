using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    Player_Movement player;
    public int speedAmount;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.SpeedUp(speedAmount);
            Destroy(gameObject);
        }
    }
}
