using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

  public Transform enemyPrefab;
  public Transform mortarCyclePrefab;
  public Transform apolcalypsePrefab;
  public Transform unityChanPrefab;
  public Transform tankDestroyerPrefab;

  public Transform spawnPoint;

  public Text waveCountdownText;

  public float timeBetweenWaves;

  public float countdown;

  private int waveIndex = 0;
  private GameState state;

  // Use this for initialization
  void Start()
  {
    state = gameObject.GetComponent<GameState>();
  }

  // Update is called once per frame
  void Update()
  {
    if (state.pause) return;
    if (countdown <= 0f)
    {
      Debug.Log("Wave Incoming!");

      StartCoroutine(spawnWave());

      countdown = timeBetweenWaves;
    }

    countdown -= Time.deltaTime;

    waveCountdownText.text = Mathf.Round(countdown).ToString();
  }

  /// <summary>
  /// Spawns a wave equal to waveIndex in number of enemies
  /// </summary>
  IEnumerator spawnWave()
  {
    waveIndex++;

    Debug.Log("Wave Incoming!");

    for (int i = 0; i < waveIndex + 3; i++)
    {
      spawnEnemy(enemyPrefab, spawnPoint);
      yield return new WaitForSeconds(0.25f);
    }
    for (int j = 0; j < (waveIndex - 3) / 2; j++)
    {
      spawnEnemy(mortarCyclePrefab, spawnPoint);
      yield return new WaitForSeconds(0.5f);
    }
    if (waveIndex > 1)
    {
      spawnEnemy(tankDestroyerPrefab, spawnPoint);
      yield return new WaitForSeconds(0.1f);
    }
    if (waveIndex > 2)
    {
      spawnEnemy(apolcalypsePrefab, spawnPoint);
      yield return new WaitForSeconds(0.1f);
    }
    if (waveIndex > 3)
    {
      spawnEnemy(unityChanPrefab, spawnPoint);
      yield return new WaitForSeconds(0.1f);
    }
  }
  public static void spawnEnemy(Transform Prefab, Transform spawnPoint)
  {
    Instantiate(Prefab, spawnPoint.position, spawnPoint.rotation);
  }

  /// <summary>
  /// Spawns a single enemy at the spawnpoint
  /// </summary>
}
