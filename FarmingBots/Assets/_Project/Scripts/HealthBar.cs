using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;

    public float currentHealth;
    private float maxHealth;

    Player_Movement player;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        maxHealth = player.health;
    }

    private void Update()
    {
        currentHealth = player.health;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

}
