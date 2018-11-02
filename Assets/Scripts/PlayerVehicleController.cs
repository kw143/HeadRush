using System.Collections;
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
	private float startingHealth = 30f;
	//how long do we want to speedboost for
	private float speedTimer;
	//are we using a controller
	private bool Xbox_One_Controller;
	//Are we turning
	private bool turning;
	//Game manager
	public GameObject gmHolder;
	private GameManager gm;
    private float hoverHeight = 2000f;
    private float dampeningFactor = 2.5f;
    private int inverseTrap = 1;
    //how long does inverse effect happen
    private float inverseTimer;
    //how long does slow effect happen
    private float slowTimer;

    // Use this for initialization
    void Start () {
		base.Rb = GetComponent<Rigidbody> ();
		base.Health = startingHealth;
		gm = gmHolder.GetComponent<GameManager> ();

	}
	
	// Update is called once per frame
	new void Update () {
        if (Health <= 0) {
            gm.EndGame();
        }
		base.Update (); //making sure we do Vehicle update
		if (speedTimer <= 0) { //if speed power up has ended
			speed = 20.0f;
            dampeningFactor = -2.5f;
            hoverHeight = 2000f;
		} else {
			speedTimer -= Time.deltaTime; //else decrease timer
		}
        if (slowTimer <= 0)
        { //if slow effect has ended
            speed = 20.0f;
            dampeningFactor = -2.5f;
            hoverHeight = 2000f;
        }
        else
        {
            slowTimer -= Time.deltaTime; //else decrease timer
        }
        if (inverseTimer <= 0)
        { //if inverseTrap has ended
            inverseTrap = 1;
        }
        else
        {
            inverseTimer -= Time.deltaTime; //else decrease timer
        }
        //This ensures they can only collide with landscape layer
        int layerMask = 1 << 9;
		//The Raycast hit for the front "Thruster"
		RaycastHit fhit;
		//The Raycast hit for the back "Thruster"
		RaycastHit bhit;
		//Casting rays to get ground information
		Physics.Raycast (transform.position + transform.forward * 5, Vector3.down, out fhit, Mathf.Infinity, layerMask);
		Physics.Raycast (transform.position - transform.forward * 5, Vector3.down, out bhit, Mathf.Infinity, layerMask);
		//Adding our own gravity
		Rb.AddForceAtPosition (Vector3.up * -5 * Mathf.Min(fhit.distance, 270), transform.position);
		//Adding thrust upward
		Rb.AddForceAtPosition (transform.up * (hoverHeight / fhit.distance), transform.position + transform.forward * 5);
		Rb.AddForceAtPosition (transform.up * (hoverHeight / bhit.distance), transform.position - transform.forward * 5);
		//Adding a dampening force
		Rb.AddForceAtPosition (Vector3.up * dampeningFactor * Rb.velocity.y, transform.transform.position);
		previousSteer = correction; //getting the previous correction for use in the method
		if (gm.Xbox_One_Controller) { //if were using a xbox controller
			throttle = Input.GetAxis ("Drive"); //left stick Up/Down
			correction = Input.GetAxis ("Steer"); //left stick Left/Right
			turn = Input.GetAxis ("Turn"); //right stick Left/Right
            if (Input.GetKey(KeyCode.JoystickButton7)) {
                gm.Pause();
            }
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

            if (Input.GetKeyDown(KeyCode.Escape)){
                gm.Pause();
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
            //transform.RotateAround (transform.position, transform.up, turn * speed / 5);
            Rb.AddTorque(inverseTrap*transform.up * turn * speed * 40);
		}
	}

	void OnTriggerEnter (Collider other) {
        if (other.GetComponent<EnemyStarter>() != null) {
            other.GetComponent<EnemyStarter>().Activate();
            Destroy(other.gameObject);
        } else if (other.GetComponent<Checkpoint>() != null) {
            other.GetComponent<Checkpoint>().checkpoint();
            Destroy(other.gameObject);
        }
		if (other.gameObject.tag == "SpeedBoost") {
            Destroy(other.gameObject);
            speed *= 1.35f;
            hoverHeight *= 1.4f;
			speedTimer = 4;
            dampeningFactor *= 2;
        } else if (other.gameObject.tag == "InverseTrap")
        {
            Destroy(other.gameObject);
            inverseTrap = -1;
            inverseTimer = 4;
        } else if (other.gameObject.tag == "SlowTrap")
        {
            Destroy(other.gameObject);
            speed *= 0.001f;
            hoverHeight *= 0.8f;
            slowTimer = 40;
        } else if (other.gameObject.tag == "FinishLine"){
            gm.EndGame();
        }
	}
}
