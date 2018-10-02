using UnityEngine;
using UnityEngine.UI;

public class MenuAnimText : MonoBehaviour {
	public string[] states;
	public Text text;
	public float switchTime = 0.25f;
	float timer;
	int state;

	void Update () {
		if (timer <= 0)
		{
			timer = switchTime;
			state = (state + 1) % states.Length;
			text.text = states[state];
		}
		timer -= Time.deltaTime;
	}
}
