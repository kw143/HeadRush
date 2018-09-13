using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
	public bool Xbox_One_Controller;
	private static float time = 0f;
	private Text Timer;
	// Use this for initialization
	void Start () {
		Timer = GameObject.Find ("Timer").GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
		if (EditorSceneManager.GetActiveScene ().name == "Demo") {
			UpdateTime ();
		}

		string[] names = Input.GetJoystickNames();
		for (int x = 0; x < names.Length; x++)
		{
			if (names [x].Length == 32) {
				
				//set a controller bool to true
				Xbox_One_Controller = true;
			} 
		}
	}

	void UpdateTime () {
		time += Time.deltaTime;
		int minutes = ((int)time) / 60;
		int seconds = ((int)time) % 60;
		int miliseconds = ((int)(time * 100)) % 100;
		string format = minutes.ToString () + ":" + seconds.ToString () + "." + miliseconds.ToString ();
		Timer.text = format;
	}
}
