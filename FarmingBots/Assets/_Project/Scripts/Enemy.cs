using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    Vector2 velocity = Vector2.zero;
    public float timeBtwAttack;
    public int damage;
    public int pickUpChance;
    public GameObject[] pickUps;
   public GameObject[] moveToPoints;
    public Transform player;
    int enemiesPresent=0;
    Transform currentMoveToPoint;
    Transform tempMoveToPoint;
    public GameObject explosionEffect;
    WaveSpawner waveSpawner;
    [Header("Score")]
    [SerializeField]
    int enemyScore;

    public virtual void Start()
    {
        moveToPoints = GameObject.FindGameObjectsWithTag("moveToPoint");
        enemiesPresent = GameObject.FindGameObjectsWithTag("Enemy").Length;
        currentMoveToPoint = enemiesPresent <= 1 ? moveToPoints[0].transform : moveToPoints[1].transform;
        tempMoveToPoint = currentMoveToPoint;
    }
    private void Awake()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health<=0)
        {
            if (waveSpawner.GetCurrentWaveIndex() != 0 && (waveSpawner.GetCurrentWaveIndex()+1)%5==0)
            {
                int randomNum = Random.Range(0, 101);
                if (randomNum < pickUpChance)
                {
                    GameObject randomPickup = pickUps[Random.Range(0, pickUps.Length)];
                    Instantiate(randomPickup, transform.position, transform.rotation);
                }
            }
            //increase score
            GameManager.Instance.UpdateScore(enemyScore);
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void SetHealth(int extraHealthAmount)
    {
        health += extraHealthAmount;
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length<=1)
        {
            if (Vector2.Distance(transform.position, moveToPoints[0].transform.position)<0.05)
            {
                tempMoveToPoint = moveToPoints[1].transform;
            }
            else if (Vector2.Distance(transform.position, moveToPoints[1].transform.position) < 0.05)
            {
                tempMoveToPoint = moveToPoints[0].transform;
            }
                transform.position = Vector2.SmoothDamp(transform.position, tempMoveToPoint.position, ref velocity, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.SmoothDamp(transform.position, currentMoveToPoint.position, ref velocity, speed * Time.deltaTime);
        }
    }
}
