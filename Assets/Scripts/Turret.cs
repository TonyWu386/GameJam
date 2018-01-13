using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

  // Stats
  public float range = 15f;
  public float turnSpeed = 10f;
  public float fireRate = 2f;
  public int cost;

  public string enemyTag = "Enemy";

  // Turrent part that rotates
  public Transform partToRotate;

  public GameObject bulletPrefab;

  // Where the bullet the fired from
  public Transform firePoint;

  // What the turret is tracking
  protected Transform target;

  // Internal countdown timer
  protected float fireCountdown = 0f;

  // Use this for initialization
  void Start ()
  {
    fireCountdown = 1f / fireRate;
    InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update ()
  {
    if (target == null)
    {
      return;
    }

    // Rotation between current and enemy
    Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);

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

  /// <summary>
  /// Auto-invoked incrementally to update target
  /// </summary>
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

    if (nearestEnemy != null && shortestDistance <= range)
    {
      target = nearestEnemy.transform;
    }
    else
    {
      target = null;
    }
  }

  /// <summary>
  /// Spawns a bullet per call
  /// </summary>
  protected void Shoot()
  {
    GameObject bulletCreate = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    Bullet bullet = bulletCreate.GetComponent<Bullet>();

    if (bullet != null)
    {
      bullet.setSeek(target);
    }
  }

  /// <summary>
  /// Comfort
  /// </summary>
  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, range);
  }
}
