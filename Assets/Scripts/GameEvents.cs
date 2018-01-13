using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GameEvents : MonoBehaviour {

  public Turret[] turret;

  public GameObject uiTurretSel;
  public Text uiHealth;
  public Text uiMoney;
  public GameObject uiGameOver;

  private static GameObject selected;
  private GameState state;
  
  void Start() {
    state = gameObject.GetComponent<GameState>();
    onStatUpdate();
  }
  
  void Update() {
    if (state.pause) return;
    if (Input.GetMouseButtonDown(0))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      
      if (Physics.Raycast(ray, out hit)) {
        if (hit.transform != null && hit.transform.parent != null && hit.transform.parent.gameObject.tag == "Node")
        {
            if (selected != null)
            {
              deselect();
            }

            selected = hit.transform.parent.gameObject;
            uiTurretSel.SetActive(true);
            uiTurretSel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 50, 0);
            ((Behaviour)selected.GetComponent("Halo")).enabled = true;
        }
        else
        {
          deselect();
        }
      }
    }
  }

  public void onTurretClick(Turret t)
  {
    int cost = t.cost;
    if (state.playerMoney >= cost)
    {
      Instantiate(t, selected.transform.position, selected.transform.rotation);
      selected.SetActive(false);
      state.playerMoney -= cost;
      onStatUpdate();
      deselect();
    }
  }

  public void onEnemyKill(Enemy t)
  {
    state.playerMoney += t.reward;
    onStatUpdate();
  }

  public void onEnemyAttack(Enemy t)
  {
    state.playerHealth = state.playerHealth - t.damage < 0 ? 0 : state.playerHealth - t.damage;
    onStatUpdate();
    if (state.playerHealth == 0)
    {
      uiGameOver.SetActive(true);
      state.pause = true;
    }
  }

  public void onStatUpdate()
  {
    uiHealth.text = state.playerHealth.ToString();
    uiMoney.text = state.playerMoney.ToString();
  }

  private void deselect()
  {
    if (selected != null)
    {
      uiTurretSel.SetActive(false);
      ((Behaviour)selected.GetComponent("Halo")).enabled = false;
      selected = null;
    }
  }
}
