using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float minX, maxX, minY, maxY;
    private Vector2 targetPosition;

    public float timeBtwSummons;
    private float summonTime;

    public float attackSpeed;
    public float stopDistance;
    private float attackTimer;
    public Enemy enemyToSummon;
 
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
    }

    // Update is called once per frame
    void Update()
    {
        if (player!=null)
        {
            if (Vector2.Distance(transform.position,targetPosition)>0.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                if (Time.time>=summonTime)
                {
                    Summon();
                    summonTime = Time.time + timeBtwSummons;
                }
            }
            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                if (Time.time >= attackTimer)
                {
                    StartCoroutine(Attack());
                    attackTimer  = Time.time + timeBtwAttack;

                }
            }
        }
        
    }
    public void Summon()
    {
        if (player!=null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }
    IEnumerator Attack()
    {
        player.GetComponent<Player_Movement>().TakeDamage(damage);
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;
        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
