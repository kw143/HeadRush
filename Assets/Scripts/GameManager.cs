using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
	public bool Xbox_One_Controller;
	public bool test_mode = false;
	private static float time = 0f;
	private static int score = 0;
    private static int timerscore = 0;
	private Text Timer;
	private Text Scoreboard;
	public float temp = 0;
	public GameObject[] countDowns = new GameObject[3];
	// Use this for initialization
	void Start () {
		Timer = GameObject.Find ("Timer").GetComponent<Text>();
		Scoreboard = GameObject.Find ("Scoreboard").GetComponent<Text> ();
		countDowns[0] = GameObject.Find ("Countdown3");
		countDowns[1] = GameObject.Find ("Countdown2");
		countDowns[2] = GameObject.Find ("Countdown1");
	}
	
	// Update is called once per frame
	void Update () {
        if (StateManager.curState == 2)
        {
            time = 0f;
            score = 0;
        }
        if (test_mode) {
			StateManager.curState = 3;
		}
		if (StateManager.curState == 3) {
            test_mode = false;
			UpdateTime ();
        } else if (StateManager.curState == 1) {
            time = 0f;
            score = 0;
        }
		temp = StateManager.levelStartTimer;
		UpdateScore ();
		string[] names = Input.GetJoystickNames();
		for (int x = 0; x < names.Length; x++)
		{
			if (names [x].Length == 32) {
				
				//set a controller bool to true
				Xbox_One_Controller = true;
			} 
		}

        if (StateManager.curState == 4) {
            int minutes = ((int)time) / 60;
            int seconds = ((int)time) % 60;
            int miliseconds = ((int)(time * 100)) % 100;
            string format = minutes.ToString() + ":" + seconds.ToString() + "." + miliseconds.ToString();
            Timer.text = "Driver Score: " + Mathf.Ceil(timerscore - minutes * 60 - seconds).ToString();
            Scoreboard.text = "Gunner Score: " + score.ToString();
        }

		//This controls the countdown at the beginning of the game
		if (StateManager.curState == 2) {
			if (StateManager.levelStartTimer >= 0) {
				StateManager.levelStartTimer -= Time.deltaTime;
				if (StateManager.levelStartTimer > 2) {
					countDowns[0].SetActive (true);
					countDowns[1].SetActive (false);
					countDowns[2].SetActive (false);
				} else if (StateManager.levelStartTimer > 1) {
					countDowns[0].SetActive (false);
					countDowns[1].SetActive (true);
					countDowns[2].SetActive (false);
				} else {
					countDowns[0].SetActive (false);
					countDowns[1].SetActive (false);
					countDowns[2].SetActive (true);
				}
			} else {
				StateManager.curState = 3;
				countDowns[2].SetActive (false);
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
	void UpdateScore(){
		Scoreboard.text = "Score: " + score.ToString ();
	}

	public static void addScore(int points) {
		score += points;
	}

    public static void addTimerScore(int points)
    {
        timerscore += points;
    }

    public void EndGame() {
        Control.EndGame();
        StateManager.curState = 4;
    }

    public void Pause() {
        if (Time.timeScale < .5f)
        {
            /*
            Time.timeScale = 1f;
            Control.Unpause();
            print("Unpause");*/
        }
        else
        {
            Time.timeScale = 0f;
            Control.Pause();
            //print("Pause");
        }

    }
}
