using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBtwShots;

    protected float shotTime;
    [SerializeField]
    protected int damage;

    protected AudioSource audioSource;
    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetMouseButton(0)&& !EventSystem.current.IsPointerOverGameObject())
        {
            
            FireButton();
        }
               Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
                transform.rotation = rotation;
    }
    public void FireButton()
    {
        if (Time.time >= shotTime)
        {
            Projectile bullet = Instantiate(projectile, shotPoint.position, transform.rotation).GetComponent<Projectile>();
            
            shotTime = Time.time + timeBtwShots;
            audioSource.PlayOneShot(audioSource.clip);   
        }
    }

}