using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apolcalypse : Enemy {
  public Transform enemyPrefab;
  public Transform mortarCyclePrefab;
  private int end = 3;
	
  /// <summary>
  /// When apolcalyse dies spawn 3 mortar cycles and 3 strykers
  /// </summary>
  override protected void onDeath()
    { 
    Instantiate(explosion, transform.position, transform.rotation);
    events.onEnemyKill(this);
    Destroy(gameObject);
    for (int i = 0; i < end; i++)
    {
      transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
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
