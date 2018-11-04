using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath2 : MonoBehaviour {

    public GameObject particles;
    public bool triggerDeath;
    public Renderer childCube;

	// Use this for initialization
	void Start () {
        particles.SetActive(false);
        triggerDeath = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(triggerDeath) {
            triggerDeath = false;
            GetComponent<Renderer>().enabled = false;
            childCube.enabled = false;
            particles.SetActive(true);
        }
	}
}
