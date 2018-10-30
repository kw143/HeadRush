using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    public int points = 0;
    private bool triggered = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void checkpoint()
    {
        if (!triggered)
        {
            GameManager.addTimerScore(points);
            triggered = true;
        }
    }
}
