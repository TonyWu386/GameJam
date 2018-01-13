
using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour {

  public float speed;
  public int health;
  public int damage;
  public int reward;

  public Transform explosion;

  // The current target the enemy is going towards
  public Transform curWP;
  public Transform nextWP;
  protected GameState state;
  protected GameEvents events;
  public bool isSlowed = false;

  // Use this for initialization
  protected void Start ()
  {
    state = GameObject.Find("/GameMaster").GetComponent<GameState>();
    events = GameObject.Find("/GameMaster").GetComponent<GameEvents>();
  }

  public void Init(Transform wp)
  {
    curWP = null;
    nextWP = wp;
    transform.LookAt(nextWP);
  }

  // Update is called once per frame
  protected void Update ()
  {
    if (state.pause) return;
    Vector3 dir = nextWP.position - transform.position;
    transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

    if (Vector3.Distance(transform.position, nextWP.position) <= 0.2f)
    {
      curWP = nextWP;
      nextWP = SpawnPoint.nextWaypoint(curWP);
      if (nextWP == null)
      {
        events.onEnemyAttack(this);
        Destroy(gameObject);
      }
      else
      {
        transform.LookAt(nextWP);
      }
    }

    if (health < 0)
    {
      onDeath();
    }
	}

  protected virtual void onDeath()
  {
    Instantiate(explosion, transform.position, transform.rotation);
    events.onEnemyKill(this);
    Destroy(gameObject);
  }
  public void Slowed()
  {
    float originalspeed = speed;
    if (!isSlowed)
    {
      speed = 5;
      StartCoroutine(restorespeed(originalspeed));
    }
  }
  IEnumerator restorespeed(float originalspeed)
  {
    if (speed >= originalspeed) {
      isSlowed = true;
    }
    while (speed < originalspeed)
    {
      speed += 1;
      yield return new WaitForSeconds(0.5f);
    }
  }
}
