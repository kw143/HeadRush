using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStarter : MonoBehaviour {
    //This is the Array to house all the enemies that will "Spawn" when crossed
    public GameObject[] enemiesToSpawn = new GameObject[3];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //The player script calls this when it hits the trigger
    public void Activate() {
        foreach (GameObject enemy in enemiesToSpawn) {
            enemy.SetActive(true);
        }
    }
}
