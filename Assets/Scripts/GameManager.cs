using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public bool Xbox_One_Controller;
	public float temp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		string[] names = Input.GetJoystickNames();
		for (int x = 0; x < names.Length; x++)
		{
			temp = names [x].Length;
			if (names [x].Length == 32) {
				
				//set a controller bool to true
				Xbox_One_Controller = true;
			} 
		}
	}
}
