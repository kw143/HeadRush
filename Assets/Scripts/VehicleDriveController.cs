using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDriveController : MonoBehaviour {
	private float correctionProgress = 0f;
	private float rotationProgress = 0f;
	private Rigidbody rb;
	private float health;
	public float temp;


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
		temp = Mathf.Abs (transform.eulerAngles.z);
		if (Health <= 0) {
			Destroy (this.gameObject);
		}
	}

	protected void MoveZDir (float speed, float throttle) {
		if (throttle > .1 && rb.velocity.z < 20 * speed) {
			rb.AddForce (transform.forward * throttle * speed * 2500 * Time.deltaTime); 
		} else if (throttle < -.1 && rb.velocity.z > 5) {
			rb.AddForce (transform.forward * throttle * speed * 4000 * Time.deltaTime);
		}
	}

	protected void MoveHorizontal (float speed, float correction, float previousSteer, bool turning, Quaternion curForward) {
		if (Mathf.Abs (correction) > .8 && (previousSteer == 0 || previousSteer == correction) && !turning) {
			rb.AddForce (transform.right * correction * speed * 1500 * Time.deltaTime);
			if (rotationProgress < .39) { 
				rotationProgress += Time.deltaTime;
				transform.RotateAround (transform.position, transform.forward, correction * -1);
			}
		} else if (transform.rotation != Quaternion.Euler (0, 0, 0) && correctionProgress < 1 && correctionProgress >= 0 && rotationProgress <= 0 && !turning) {
			correctionProgress += Time.deltaTime;
			transform.rotation = Quaternion.Euler (new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0));
		} else if (correctionProgress >= 1) {
			correctionProgress = 0;
		} else if (rotationProgress != 0) {
			rotationProgress = 0;
		}
	}
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "Bullet(Clone)") {
			if (this.gameObject.tag != "Player") {
				col.gameObject.transform.position = new Vector3 (-100000, 0, 0);
				this.health -= col.gameObject.GetComponent<BulletBehavior> ().Damage;
			}
		} else if (col.gameObject.tag == "Obstacle") {
			this.health = 0;
			Destroy (col.gameObject);
		} else if (col.gameObject.tag == "Landscape") {
			this.health = 0;
		} 
	}


}
