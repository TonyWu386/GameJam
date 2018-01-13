
using UnityEngine;

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

  // Use this for initialization
  protected void Start ()
  {
    state = GameObject.Find("/GameMaster").GetComponent<GameState>();
    events = GameObject.Find("/GameMaster").GetComponent<GameEvents>();
  }

  public void Init(Transform wp)
  {
    curWP = wp;
    nextWP = SpawnPoint.nextWaypoint(curWP);
    transform.LookAt(nextWP);
  }

  // Update is called once per frame
  protected void Update ()
  {
    if (state.pause) return;
    Vector3 dir = curWP.position - transform.position;
    transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

    if (Vector3.Distance(transform.position, curWP.position) <= 0.2f)
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
        transform.LookAt(curWP);
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
}
