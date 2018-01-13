using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apolcalypse : Enemy {
  public Enemy enemyPrefab;
  public Enemy mortarCyclePrefab;

  override protected void onDeath()
  {
    Instantiate(explosion, transform.position, transform.rotation);
    events.onEnemyKill(this);
    for (int i = 0; i < 3; i++)
    {
      StartCoroutine(deathSpawn());
    }
    Destroy(gameObject);
  }

  IEnumerator deathSpawn()
  {
    Enemy enemyUnit = Instantiate(enemyPrefab, transform.position, transform.rotation);
    enemyUnit.Init(nextWP);
    Enemy enemyMortar = Instantiate(mortarCyclePrefab, transform.position, transform.rotation);
    enemyMortar.Init(nextWP);
    yield return new WaitForSeconds(0.25f);
  }
}
