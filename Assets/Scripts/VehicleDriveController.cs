using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDriveController : MonoBehaviour {
	private float correctionProgress = 0f;
	private float rotationProgress = 0f;
	private Rigidbody rb;
	private float health;

	public Rigidbody Rb {
		get {
			return rb;
		}
		set {
			rb = value;
		}
	}

	public float Health {
		get {
			return health;
		}
		set {
			health = value;
		}
	}

	//private  playerVehicle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		if (Health <= 0) {
			Destroy (this.gameObject);
		}
	}

	protected void MoveZDir (float speed, float throttle) {
		if (throttle > .1 && rb.velocity.z < 20 * speed) {
			rb.AddForce (transform.forward * throttle * speed * 250 * Time.deltaTime); 
		} else if (throttle < -.1 && rb.velocity.z > 5) {
			rb.AddForce (transform.forward * throttle * speed * 400 * Time.deltaTime);
		}
	}
	protected void MoveHorizontal (float speed, float correction, float previousSteer) {
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
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "Bullet(Clone)") {
			col.gameObject.transform.position = new Vector3 (-100000, 0, 0);
			this.health -= col.gameObject.GetComponent<BulletBehavior> ().Damage;
		} else if (col.gameObject.tag == "Obstacle") {
			this.health = 0;
			Destroy (col.gameObject);
		} else if (col.gameObject.tag == "Landscape") {
			this.health = 0;
		}
	}


}
