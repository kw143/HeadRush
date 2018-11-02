using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control: MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	public void NextScene()
	{
		StateManager.curState = 2;
		StateManager.levelStartTimer = 3.0f;
		SceneManager.LoadScene("MainLevel");
     
	}

    public static void EndGame()
    {
        SceneManager.LoadScene("EndGameScreen");
    }

    public void MainMenu()
    {
        StateManager.curState = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ControlMenu() {
        SceneManager.LoadScene("Controls");
    }

    public static void Pause() {
        SceneManager.LoadScene("Pause");
    }

    public static void Unpause() {
        
    }
}


