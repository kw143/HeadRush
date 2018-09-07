using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVehicleController : VehicleDriveController {
	private float speed = 10.0f;
	private float previousSteer;
	private float throttle = 0f;
	private float correction = 0f;
	private float turn = 0f;
	private float startingHealth = 10f;
	private bool Xbox_One_Controller;
	private bool turning;
	public GameObject gmHolder;
	private GameManager gm;
	// Use this for initialization
	void Start () {
		base.Rb = GetComponent<Rigidbody> ();
		base.Health = startingHealth;
		gm = gmHolder.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		previousSteer = correction;
		if (gm.Xbox_One_Controller) {
			throttle = Input.GetAxis ("Drive");
			correction = Input.GetAxis ("Steer");
			turn = Input.GetAxis ("Turn");
		} else {
			if (Input.GetKey (KeyCode.W)) {
				throttle = 1;
			} else if (Input.GetKey (KeyCode.S)) {
				throttle = -1;
			}
			if (Input.GetKey (KeyCode.D)) {
				correction = 1;
			} else if (Input.GetKey (KeyCode.A)) {
				correction = -1;
			}
		}
		turning = Mathf.Abs (turn) > .1;
		TurnRotate (turn, speed);
		MoveZDir (speed, throttle);
		MoveHorizontal (speed, correction, previousSteer, turning);

		throttle = 0;
		correction = 0;
	}

	void TurnRotate (float turn, float speed) {
		Rb.AddForce (transform.up * turn * speed * Time.deltaTime * 10);
	}


}
