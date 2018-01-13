using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
  public Transform[] wayPointStart;
  public int priority = 1;

  public void spawnEnemy(Enemy enemyPrefab)
  {
    Enemy enemyUnit = Instantiate(enemyPrefab, transform.position, transform.rotation);
        int i = Random.Range(0, wayPointStart.Length);
        enemyUnit.Init(wayPointStart[i]);
  }

  public static Transform nextWaypoint(Transform wp)
  {
    if (wp.childCount == 0)
    {
      int index = wp.GetSiblingIndex();
      return index + 1 == wp.parent.childCount ? null : wp.parent.GetChild(index + 1);
    }
    else
    {
      return wp.GetChild(0).transform;
    }
  }
}
