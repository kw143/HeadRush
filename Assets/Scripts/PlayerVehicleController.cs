using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVehicleController : VehicleDriveController {
	private float speed = 10.0f;
	private float previousSteer;
	private float throttle = 0f;
	private float correction = 0f;
	private float startingHealth = 10f;

	// Use this for initialization
	void Start () {
		base.Rb = GetComponent<Rigidbody> ();
		base.Health = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		previousSteer = correction;
		throttle = Input.GetAxis ("Drive");
		correction = Input.GetAxis ("Steer");
		MoveZDir (speed, throttle);
		MoveHorizontal (speed, correction, previousSteer);
	}


}
