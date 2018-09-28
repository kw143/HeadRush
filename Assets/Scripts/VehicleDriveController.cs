using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDriveController : MonoBehaviour {
	private float correctionProgress = 0f;
	private float rotationProgress = 0f;
	private Rigidbody rb;
	private float health;
	public bool upRayHit;
	public bool downRayHit;
	public float changeTimer = 0;

	/*
	 * Getter/Setter for the rigibody
	 */ 
	public Rigidbody Rb {
		get {
			return rb;
		}
		set {
			rb = value;
		}
	}
	/*
	 * Getter/Setter for the Health
	 */ 
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
		int layerMask = 1 << 9;
		RaycastHit hit;

		Physics.Raycast (transform.position + transform.forward, Vector3.down, out hit, Mathf.Infinity, layerMask);
		/*downRayHit = Physics.Raycast (transform.position + transform.forward - 10 * transform.up, Vector3.down, 20);
			
		if (upRayHit) {
			rb.constraints = RigidbodyConstraints.None;
			if (transform.rotation.x > -10) {
				transform.RotateAround (transform.position, transform.right, Time.deltaTime * -20);
			}
			transform.Translate (transform.up * Time.deltaTime * 20);
		} else if (!upRayHit && downRayHit) {
			if (transform.rotation.x < 0) {
				transform.RotateAround (transform.position, transform.right, Time.deltaTime * 20);
			}
			rb.constraints = RigidbodyConstraints.FreezePositionY;
		}
		if (!downRayHit) {
			rb.useGravity = true;
		} else {
			rb.useGravity = false;
		}*/
	}

	/*
	 * This method it what adds the forward direction.
	 * You cannot move unless the game state is set to movnig,
	 * allowing for a start of game countdown
	 * Speed is a constant associated with a type of vehicle
	 * throttle is how hard the user is pushing forward
	 */ 
	protected void MoveZDir (float speed, float throttle) {
		if (StateManager.curState == 3) {
			if (throttle > .1 && rb.velocity.z < 20 * speed) {
				rb.AddForce (transform.forward * throttle * speed * 2500 * Time.deltaTime); 
			} else if (throttle < -.1 && rb.velocity.z > 5) {
				rb.AddForce (transform.forward * throttle * speed * 4000 * Time.deltaTime);
			}
		}
	}
	/*
	 * This method is what controls the side sweeping motion
	 * While the key is held down, your vehicle banks and moves in
	 * that direction.
	 * After they are down banking, the vehicle rights itself
	 * Speed is a constant associated with a type of vehicle
	 * Correction is how much the player wants to move sideways
	 * Previous steer what the player inputed last frame.
	 * turning makes sure the player is not turning while banking
	 * 
	 */ 
	protected void MoveHorizontal (float speed, float correction, float previousSteer, bool turning) {
		//making sure were in the correct state
		if (StateManager.curState == 3) {
			//solves issues of player banking getting stuck
			if (Mathf.Abs (correction) > .8 && (previousSteer == 0 || previousSteer == correction) && !turning) {
				rb.AddForce (transform.right * correction * speed * 1500 * Time.deltaTime);
				if (rotationProgress < .39) { 
					rotationProgress += Time.deltaTime;
					transform.RotateAround (transform.position, transform.forward, correction * -1);
				}
			} else if (transform.rotation != Quaternion.Euler (0, 0, 0) && correctionProgress < 1 && correctionProgress >= 0 && rotationProgress <= 0 && !turning) {
				//Putting the players rotation back to 0 degrees
				correctionProgress += Time.deltaTime;
				transform.rotation = Quaternion.Euler (new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0));
			} else if (correctionProgress >= 1) {
				correctionProgress = 0;
			} else if (rotationProgress != 0) {
				rotationProgress = 0;
			}
		}
	}
	/*
	 * Collision detection for everyone, so has to be generic
     * Col is the object this collided with us
	 */ 
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
