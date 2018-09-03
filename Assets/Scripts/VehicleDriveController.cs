using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDriveController : MonoBehaviour {
	private float speed = 10.0f;
	private float throttle = 0f;
	private float correction = 0f;
	private float correctionProgress = 0f;
	private float rotationProgress = 0f;
	public float temp = 0f;
	private Rigidbody rb;
	private float previousSteer;

	//private  playerVehicle;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		previousSteer = correction;
		temp = rb.velocity.z;
		throttle = Input.GetAxis ("Drive");
		correction = Input.GetAxis ("Steer");
		if (throttle > .1 && rb.velocity.z < 20 * speed) {
			rb.AddForce (transform.forward * throttle * speed * 250 * Time.deltaTime); 
		} else if (throttle < -.1 && rb.velocity.z > 5) {
			rb.AddForce (transform.forward * throttle * speed * 400 * Time.deltaTime);
		}
		if (Mathf.Abs (correction) > .1 && (previousSteer == 0 || previousSteer == correction)) {
			rb.AddForce (transform.right * correction * speed * 150 * Time.deltaTime);
			if (transform.rotation.z < Quaternion.Euler (0, 0, 7).z && transform.rotation.z > Quaternion.Euler (0, 0, -7).z && rotationProgress >= 0 && rotationProgress < 1) { 
				rotationProgress += Time.deltaTime;
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, correction * -7), rotationProgress);
			}
		} else if (transform.rotation != Quaternion.Euler (0, 0, 0) && correctionProgress < 1 && correctionProgress >= 0 && rotationProgress <= 0) {
			correctionProgress += Time.deltaTime;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 0), correctionProgress);
		} else if (correctionProgress >= 1) {
			correctionProgress = 0;
		} else if (rotationProgress != 0) {
			rotationProgress = 0;
		}


	}
}
