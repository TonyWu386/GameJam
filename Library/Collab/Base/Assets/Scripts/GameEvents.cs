using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


public class GameEvents : MonoBehaviour {

  public GameObject turret1;
  public GameObject turret2;

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
    Rect uiRect = new Rect(uiTurretSel.transform.position.x, uiTurretSel.transform.position.y, uiTurretSel.GetComponent<RectTransform>().rect.width, uiTurretSel.GetComponent<RectTransform>().rect.height);
    if (Input.GetMouseButtonDown(0) && !uiRect.Contains(Input.mousePosition))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit))
      {
        if (hit.transform != null && hit.transform.parent != null && hit.transform.parent.gameObject.tag == "Node")
        {
          
            if (selected != null)
            {
              onDeselect();
            }

            selected = hit.transform.parent.gameObject;
            uiTurretSel.SetActive(true);
            uiTurretSel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 50, 0);
            ((Behaviour)selected.GetComponent("Halo")).enabled = true;
        }
        else
        {
          onDeselect();
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
      onDeselect();
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

  private void onDeselect()
  {
    if (selected != null)
    {
      uiTurretSel.SetActive(false);
      ((Behaviour)selected.GetComponent("Halo")).enabled = false;
      selected = null;
    }
  }

  private void onStatUpdate()
  {
    uiHealth.text = state.playerHealth.ToString();
    uiMoney.text = state.playerMoney.ToString();
  }
}
