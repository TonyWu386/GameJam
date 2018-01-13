using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

  protected Transform target;

  public int damageDone = 3;
  public float speed;

	// Use this for initialization
	void Start ()
  {
		
	}
	
	// Update is called once per frame
	protected void Update ()
  {
    if (target == null)
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
	}

  /// <summary>
  /// Bullet has hit a target
  /// </summary>
  protected void HitTarget()
  {
    Destroy(gameObject);
    target.gameObject.GetComponent<Enemy>().health -= damageDone;
  }

  /// <summary>
  /// Allows for setting the target this bullet will seek.
  /// </summary>
  /// <param name="_target"></param>
  public void setSeek(Transform _target)
  {
    target = _target;
  }
}
