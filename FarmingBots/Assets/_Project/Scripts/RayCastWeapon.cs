using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCastWeapon : Weapon
{
	public GameObject impactEffect;
	public LineRenderer lineRenderer;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			if (Time.time >= shotTime)
			{
				StartCoroutine(Shoot());
				shotTime = Time.time + timeBtwShots;
			}
		}
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		transform.rotation = rotation;
	}

	IEnumerator Shoot()
	{
		

		RaycastHit2D hitInfo = Physics2D.Raycast(shotPoint.position, shotPoint.up);
		audioSource.PlayOneShot(audioSource.clip);
		if (hitInfo)
		{
			Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
			EnemyBullet enemyBullet = hitInfo.transform.GetComponent<EnemyBullet>();
			Boss bossEnemy = hitInfo.transform.GetComponent<Boss>();
			if (enemy != null)
			{
				enemy.TakeDamage(damage);
			}
			else if (bossEnemy!=null)
            {
				bossEnemy.TakeDamage(damage);
            }
			else if (enemyBullet!=null)
            {
				Destroy(enemyBullet.gameObject);
            }
			Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
			lineRenderer.SetPosition(0, shotPoint.position);
			lineRenderer.SetPosition(1, hitInfo.point);
		}
		else
		{
			lineRenderer.SetPosition(0, shotPoint.position);
			lineRenderer.SetPosition(1, shotPoint.position + shotPoint.up* 100);
		}

		lineRenderer.enabled = true;

		yield return new WaitForSeconds(0.1f);

		lineRenderer.enabled = false;
	}
}
