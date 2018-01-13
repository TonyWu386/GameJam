using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour {

    public List<WaveSpawner> waveSpawnerTracker = new List<WaveSpawner>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    static Transform getWaypoint(int i, int id) {
        //GameObject[] w1 = GameObject.FindGameObjectsWithTag("Waypoint");
        //Instantiate()
        GameObject spawn_list = GameObject.FindGameObjectsWithTag();
        return id < 10 ? w1.children[i] : w2.GetComponentInChildren[i];
       
    }*/
}
