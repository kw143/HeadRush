﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVehicleController : VehicleDriveController {
	//constant for the player vehicle
	//If you change this, change it also at beginning of start
	public float speed = 20.0f;
	//how much we turned last frame
	private float previousSteer;
	//Player inputs
	private float throttle = 0f;
	private float correction = 0f;
	private float turn = 0f;
	//Constant for the player, will need to be changed to not die instantly
	private float startingHealth = 10f;
	//how long do we want to boost for
	private float speedTimer;
	//are we using a controller
	private bool Xbox_One_Controller;
	//Are we turning
	private bool turning;
	//Game manager
	public GameObject gmHolder;
	private GameManager gm;



	// Use this for initialization
	void Start () {
		base.Rb = GetComponent<Rigidbody> ();
		base.Health = startingHealth;
		gm = gmHolder.GetComponent<GameManager> ();

	}
	
	// Update is called once per frame
	new void Update () {
		base.Update (); //making sure we do Vehicle update
		if (speedTimer <= 0) { //if speed power up has ended
			speed = 20.0f;
		} else {
			speedTimer -= Time.deltaTime; //else decrease timer
		}

		previousSteer = correction; //getting the previous correction for use in the method
		if (gm.Xbox_One_Controller) { //if were using a xbox controller
			throttle = Input.GetAxis ("Drive"); //left stick Up/Down
			correction = Input.GetAxis ("Steer"); //left stick Left/Right
			turn = Input.GetAxis ("Turn"); //right stick Left/Right
		} else {
			if (Input.GetKey (KeyCode.W)) {
				throttle = 1;
			} else if (Input.GetKey (KeyCode.S)) {
				throttle = -1;
			}
			if (Input.GetKey (KeyCode.Q)) {
				correction = -1;
			} else if (Input.GetKey (KeyCode.E)) {
				correction = 1;
			}

			if (Input.GetKey (KeyCode.A)) {
				turn = -1;
			} else if (Input.GetKey (KeyCode.D)) {
				turn = 1;
			}
		}
		//useful in two spots, here and to make sure we're allowed to bank
		turning = Mathf.Abs (turn) > .1;
		if (turning) {
			TurnRotate (turn, speed);
		}
		//move forward
		MoveZDir (speed, throttle);
		//bank sideways
		MoveHorizontal (speed, correction, previousSteer, turning);
		//reset everything for next fram
		throttle = 0; 
		correction = 0;
		turn = 0;
	}


	/*
	 * This method is what turns the vehicles about the y axis
	 * turn is the player input
	 * speed is a constant defined for a particular vehicle
	 */ 
	void TurnRotate (float turn, float speed) {
		if (StateManager.curState == 3) {
			transform.RotateAround (transform.position, transform.up, turn * speed / 5);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "SpeedBoost") {
			speed *= 2;
			speedTimer = 10;
			Destroy (other.gameObject);
		}
	}
}
