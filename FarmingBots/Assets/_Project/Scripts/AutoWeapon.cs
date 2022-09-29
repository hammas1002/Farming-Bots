using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBtwShots;
    [SerializeField]
    private float shotTime;
    [SerializeField]
    private int damage;

    Transform player;
    private void Awake()
    {
        player = FindObjectOfType<Player_Movement>().transform;
    }
    private void OnEnable()
    {
        player = FindObjectOfType<Player_Movement>().transform;
    }

    void Update()
    {
        WeaponRotation();
        FireButton();
    }

    public void FireButton()
    {
        if (player == null) return;
        if (Time.time >= shotTime)
        {
            EnemyBullet bullet = Instantiate(projectile, shotPoint.position, transform.rotation).GetComponent<EnemyBullet>();
            shotTime = Time.time + timeBtwShots;

        }
    }
    public void WeaponRotation()
    {
        if (player == null) return;
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }
}
