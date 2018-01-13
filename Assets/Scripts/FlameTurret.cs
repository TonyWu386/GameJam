using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTurret : Turret
{

  void Start()
  {
    fireCountdown = 1f / fireRate;
    InvokeRepeating("UpdateTarget", 0f, 2f);
  }

  void Update()
  {

    if (target == null) return;

    // Rotation between current and enemy
    Quaternion lookRotation = Quaternion.LookRotation(target.position - partToRotate.position);

    // Smooth rotation
    Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

    // We only rotate side-to-side
    partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    if (fireCountdown <= 0f)
    {
      Shoot();
      fireCountdown = 1f / fireRate;
    }

    fireCountdown -= Time.deltaTime;

  }

  protected void Shoot()
  {
    Instantiate(bulletPrefab, firePoint.position, partToRotate.rotation);
    target.gameObject.GetComponent<Enemy>().health -= 10;
  }


  void UpdateTarget()
  {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    float shortestDistance = Mathf.Infinity;
    GameObject nearestEnemy = null;

    foreach (var enemy in enemies)
    {
      float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
      if (distanceToEnemy < shortestDistance)
      {
        shortestDistance = distanceToEnemy;
        nearestEnemy = enemy;
      }
    }

    if (nearestEnemy != null)
    {
      target = nearestEnemy.transform;
    }
    else
    {
      target = null;
    }
  }

}
