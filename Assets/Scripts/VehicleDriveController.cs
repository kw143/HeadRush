using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDriveController : MonoBehaviour {
	private float correctionProgress = 0f;
	private float rotationProgress = 0f;
	private Rigidbody rb;
	public float health;
	public bool upRayHit;
	public bool downRayHit;
	public float changeTimer = 0;
    public bool hitCapable = true;

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
	
    protected void Death() {
        //Destroy(this.gameObject);
    }

	// Update is called once per frame
	protected void Update () {
		
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
		if (StateManager.curState == 3) {
			//make sure we only detect landscape
			int layerMask = 1 << 9;
			//left side of vehicle
			RaycastHit lhit;
			//right side of vehicle
			RaycastHit rhit;
			//Getting the ground
			Physics.Raycast (transform.position + 2 * transform.right, Vector3.down, out rhit, Mathf.Infinity, layerMask);
			Physics.Raycast (transform.position + -2 * transform.right, Vector3.down, out lhit, Mathf.Infinity, layerMask);
			//if we turning right
			if (correction > .8 && !turning) {
				Rb.AddForceAtPosition (transform.up * 1 / rhit.distance, transform.position + 2 * transform.right);
				Rb.AddForceAtPosition (transform.up * 1 / lhit.distance * 300, transform.position + -2 * transform.right);
				Rb.AddForce (transform.right * speed * correction * 20);
			//left
			} else if (correction < -.8) {
				Rb.AddForceAtPosition (transform.up * 1 / rhit.distance * 300, transform.position + 2 * transform.right);
				Rb.AddForceAtPosition (transform.up * 1 / lhit.distance, transform.position + -2 * transform.right);
                Rb.AddForce (transform.right * speed * correction * 20);
			//must bring balance to the forces
			} else {
				Rb.AddForceAtPosition (transform.up * 1 / rhit.distance * 15, transform.position + 2 * transform.right);
				Rb.AddForceAtPosition (transform.up * 1 / lhit.distance * 15, transform.position + -2 * transform.right);

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
            if (this.gameObject.tag == "Player")
            {
                this.gameObject.GetComponent<AudioSource>().Play();
            }
			this.health -= 10;
			Destroy (col.gameObject);
		} else if (col.gameObject.tag == "Landscape") {
			this.health -= 10;
        } else if (col.gameObject.tag == "Player") {
            if (hitCapable)
            {
                hitCapable = false;
                col.gameObject.GetComponent<PlayerVehicleController>().Health -= 5;
                this.Health -= 10;
            }
        }
	}


}
