using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTurret : Turret {
  public Explosion iceAOEPrefab;
	// Update is called once per frame
	protected void Update () {
    float fireCountdown = 0f;
    if (fireCountdown <= 0f)
    {
      Shoot();
      fireCountdown = 1f / fireRate;
    }

    fireCountdown -= Time.deltaTime;
  }
  private new void Shoot()
  {
    if (target == null)
    {
      return;
    }
    Instantiate(iceAOEPrefab, transform.position, transform.rotation);

    List<Transform> withinRange = new List<Transform>();

    foreach (GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
    {
      float originalspeed = enemyObj.GetComponent<Enemy>().speed;
      if ((transform.position - enemyObj.transform.position).magnitude < range)
      {
        enemyObj.GetComponent<Enemy>().Slowed();
      }
    }
  }
}
