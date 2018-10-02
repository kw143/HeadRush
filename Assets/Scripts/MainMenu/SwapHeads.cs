using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapHeads : MonoBehaviour {
	public GameObject[] states;
	public float switchTime = 0.25f;
	float timer;
	int state;

	void Update()
	{
		if (timer <= 0)
		{
			states[state].SetActive(false);
			timer = switchTime;
			state = (state + 1) % states.Length;
			states[state].SetActive(true);
		}
		timer -= Time.deltaTime;
	}
}
