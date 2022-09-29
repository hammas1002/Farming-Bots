using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Player_Movement player;
    public int damage;
    public float lifeTime;
    public float speed;

    public GameObject explosion;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        Invoke(nameof(DestroyProjectile), lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damage);
            DestroyProjectile();
        }

    }
    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
