using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apolcalypse : Enemy {
  public Transform enemyPrefab;
  public Transform mortarCyclePrefab;

  override protected void onDeath()
  {
    Vector3 dir = curWP.position - transform.position;
    Instantiate(explosion, transform.position, transform.rotation);
    transform.Translate(dir.normalized * speed * (Time.deltaTime + 1), Space.World);
    Instantiate(explosion, transform.position, transform.rotation);
    transform.Translate(dir.normalized * speed * (Time.deltaTime + 1), Space.World);
    Instantiate(explosion, transform.position, transform.rotation);
    events.onEnemyKill(this);
    Destroy(gameObject);
    for (int i = 0; i < 3; i++)
    {
      transform.Translate(dir.normalized * speed * (Time.deltaTime + i), Space.World);
      StartCoroutine(deathSpawn());
    }
  }

  IEnumerator deathSpawn()
  {
    WaveSpawner.spawnEnemy(enemyPrefab, transform);
    WaveSpawner.spawnEnemy(mortarCyclePrefab, transform);
    yield return new WaitForSeconds(0.25f);
  }
}
