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
        Scene curscene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(curscene.buildIndex);
        SceneManager.LoadSceneAsync("MainLevel");


     
	}

    public static void EndGame()
    {
        SceneManager.LoadSceneAsync("EndGameScreen");
    }

    public void MainMenu()
    {
        StateManager.curState = 1;
        SceneManager.LoadSceneAsync("Menu");
    }

    public void ControlMenu() {
        SceneManager.LoadSceneAsync("Controls");
    }

    public static void Pause() {
        SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
    }

    public static void Unpause() {
        SceneManager.UnloadSceneAsync("Pause");
    }

    public void Buttonunpause() {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Pause");
    }
}


