using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVehicleController : VehicleDriveController {
	public float speed = 10.0f;
	private float previousSteer;
	private float throttle = 0f;
	private float correction = 0f;
	private float turn = 0f;
	private float startingHealth = 10f;
	private float speedTimer;
	private bool Xbox_One_Controller;
	private bool turning;
	public GameObject gmHolder;
	private GameManager gm;
	private Quaternion curForward;


	// Use this for initialization
	void Start () {
		base.Rb = GetComponent<Rigidbody> ();
		base.Health = startingHealth;
		gm = gmHolder.GetComponent<GameManager> ();

	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		if (speedTimer <= 0) {
			speed = 10.0f;
		} else {
			speedTimer -= Time.deltaTime;
		}
		curForward = transform.rotation;

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
		turning = Mathf.Abs (turn) > .1;
		if (turning) {
			TurnRotate (turn, speed);
		}
		MoveZDir (speed, throttle);
		MoveHorizontal (speed, correction, previousSteer, turning, curForward);

		throttle = 0;
		correction = 0;
		turn = 0;
	}

	void TurnRotate (float turn, float speed) {

		transform.RotateAround (transform.position, transform.up, turn * speed / 5);
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "SpeedBoost") {
			speed *= 2;
			speedTimer = 10;
			Destroy (other.gameObject);
		}
	}
}
