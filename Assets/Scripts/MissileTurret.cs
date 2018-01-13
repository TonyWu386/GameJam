using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : Turret
{
  void Update()
  {
    if (fireCountdown >= 1f / (fireRate * 2) && target != null)
    {
      transform.Find("PartsToRotate").Find("Missile").gameObject.SetActive(false);
    }
    else
    {
      transform.Find("PartsToRotate").Find("Missile").gameObject.SetActive(true);
    }

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


}
