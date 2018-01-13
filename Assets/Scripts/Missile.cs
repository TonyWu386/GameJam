using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
  public Transform missileExplosionPrefab;

  // Radius of splash damage
  public float splashRadius;

  // How long to fly before disappearing
  public float range;

  void Update()
  {
    if (target == null || range <= 0)
    {
      Destroy(gameObject);
      return;
    }

    Vector3 dir = target.position - transform.position;
    float distanceThisFrame = speed * Time.deltaTime;

    if (dir.magnitude <= distanceThisFrame)
    {
      HitTarget();
      return;
    }

    transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    // Rotation between current and enemy
    Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);

    // Smooth rotation
    Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;

    transform.rotation = Quaternion.Euler(rotation);

    range -= Time.deltaTime;
  }

  /// <summary>
  /// Missile cause damamge on all enemies with its splash radius
  /// </summary>
  protected new void HitTarget()
  {
    Instantiate(missileExplosionPrefab, transform.position, transform.rotation);

    List<Transform> withinRange = new List<Transform>();

    foreach (GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
    {
      if ((transform.position - enemyObj.transform.position).magnitude < splashRadius)
      {
        enemyObj.GetComponent<Enemy>().health -= damageDone;
      }
    }

    Destroy(gameObject);
  }
}
