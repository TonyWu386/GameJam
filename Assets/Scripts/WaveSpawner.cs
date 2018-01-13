using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{/*
    public Transform enemyPrefab;
    public Transform mortarCyclePrefab;
    public Transform apolcalypsePrefab;
    public Transform unityChanPrefab;
    public Transform tankDestroyerPrefab;*/
 
      public Enemy enemyPrefab;
      public Enemy mortarCyclePrefab;
      public Enemy apolcalypsePrefab;
      public Enemy unityChanPrefab;
      public Enemy tankDestroyerPrefab;

    //public SpawnPoint spawnPoint;
    public SpawnPoint[] spawnPoint;
  public Text waveCountdownText;

  public float timeBetweenWaves;

  public float countdown;

  private int waveIndex = 0;
  private GameState state;
    private int priorityCount = 0;
  // Use this for initialization
  void Start()
  {
    state = gameObject.GetComponent<GameState>();
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            priorityCount += spawnPoint[i].GetComponent<SpawnPoint>().priority;
        }
  }

  // Update is called once per frame
  void Update()
  {
    if (state.pause) return;
    if (countdown <= 0f)
    {
      StartCoroutine(spawnWave());

      countdown = timeBetweenWaves;
    }

    countdown -= Time.deltaTime;

    waveCountdownText.text = Mathf.Round(countdown).ToString();
  }
  
  IEnumerator spawnWave()
  {
    waveIndex++;

    for (int i = 0; i < waveIndex + 3; i++)
    {
            spawnEnemy(enemyPrefab);
      //spawnPoint.spawnEnemy(enemyPrefab);
      yield return new WaitForSeconds(0.25f);
    }
    for (int j = 0; j < (waveIndex - 3) / 2; j++)
    {
            spawnEnemy(mortarCyclePrefab);
      //spawnPoint.spawnEnemy(mortarCyclePrefab);
      yield return new WaitForSeconds(0.5f);
    }
    if (waveIndex > 1)
    {
            spawnEnemy(tankDestroyerPrefab);
      //spawnPoint.spawnEnemy(tankDestroyerPrefab);
      yield return new WaitForSeconds(0.1f);
    }
    if (waveIndex > 2)
    {
            spawnEnemy(apolcalypsePrefab);
      //spawnPoint.spawnEnemy(apolcalypsePrefab);
      yield return new WaitForSeconds(0.1f);
    }
    if (waveIndex > 0)
    {
            spawnEnemy(unityChanPrefab);
      //spawnPoint.spawnEnemy(unityChanPrefab);
      yield return new WaitForSeconds(0.1f);
    }
  }
  //public static void spawnEnemy(Transform Prefab/*, Transform spawnPoint*/)
 // {
 //   Instantiate(Prefab, spawnPoint.position, spawnPoint.rotation);
 // }

   
    void spawnEnemy(Enemy preFab)
    {
        int priorityReference = spawnPoint[0].priority;
        int i = Random.Range(0, priorityCount + 1);
        int counter = 0;
        while (i > priorityReference)
        {
            counter += 1;
            priorityReference += spawnPoint[counter].priority;
        }
        spawnPoint[counter].spawnEnemy(preFab);
    }
}
