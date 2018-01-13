
using UnityEngine;

public class Enemy : MonoBehaviour {

  public float speed;
  public int health;
  public int damage;
  public int reward;
  public bool slowed = false;

  public Transform explosion;

  // The current target the enemy is going towards
  protected Transform target;
  protected int wavepointIndex;
  protected GameState state;
  protected GameEvents events;
  protected Vector3 dir;

  // Use this for initialization
  protected void Start ()
  {
    wavepointIndex = 0;
    target = Waypoints.points[0];
    transform.LookAt(target);
    state = GameObject.Find("/GameMaster").GetComponent<GameState>();
    events = GameObject.Find("/GameMaster").GetComponent<GameEvents>();
  }
	
	// Update is called once per frame
	protected void Update ()
  {
    if (state.pause) return;
    dir = target.position - transform.position;
    transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

    if (Vector3.Distance(transform.position, target.position) <= 0.2f)
    {
      GetNextWaypoint();
    }
    // on death
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
    return;
  }
  protected void GetNextWaypoint()
  {
    if (wavepointIndex >= Waypoints.points.Count - 1)
    {
      events.onEnemyAttack(this);
      Destroy(gameObject);
      return;
    }

    wavepointIndex++;
    // Set the new target
    target = Waypoints.points[wavepointIndex];
    // Turn towards the new target
    transform.LookAt(target);
  }
}
