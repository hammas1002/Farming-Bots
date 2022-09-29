using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    private int halfHealth;
    public int damage;

    public float speed;
    Vector2 velocity = Vector2.zero;

    GameManager gameManager;
    WaveSpawner waveSpawner;

    GameObject moveToPoint;
     public GameObject[] moveToPoints;

    private Animator anim;
    private Slider healthBar;
    public GameObject explosionEffect;

    [Header("Score")]
    [SerializeField]
    int enemyScore;

    private void Awake()
    {
        moveToPoint = GameObject.FindGameObjectWithTag("BossMoveToPoint");
        gameManager = FindObjectOfType<GameManager>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        healthBar = FindObjectOfType<Slider>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        healthBar = FindObjectOfType<Slider>();
        halfHealth = health / 2; 
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, moveToPoint.transform.position, ref velocity, speed * Time.deltaTime);
    }

    public void TakeDamage(int damageAmount)
    {

        health -= damageAmount;
        healthBar.value = health;
        if (health <= 0)
        {
            healthBar.gameObject.SetActive(false);
            //restart waves
            waveSpawner.RestartWaves();

            //increase score
            GameManager.Instance.UpdateScore(enemyScore);
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (health <= halfHealth)
        {
          //  anim.SetTrigger("stage2");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_Movement>().TakeDamage(damage);
        }
    }
}
