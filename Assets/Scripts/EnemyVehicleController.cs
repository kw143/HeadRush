using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVehicleController : VehicleDriveController {

	public float speed = 19f;
	private float correction;

	public GameObject player;
	// Use this for initialization
	void Start () {
        base.health = 10f;
		base.Rb = gameObject.GetComponent<Rigidbody> ();

	}

    new void Death() {
        this.GetComponent<EnemyDeath>().triggerDeath = true;
        speed = 0;
        Destroy(this.gameObject);
    }

	// Update is called once per frame
	new void Update () {
		base.Update();
        if (base.Health <= 0)
        {
            Death();
        }
        MoveZDir(speed, 1);
		//This ensures they can only collide with landscape layer
		int layerMask = 1 << 9;
		//The Raycast hit for the front "Thruster"
		RaycastHit fhit;
		//Casting rays to get ground information
		Physics.Raycast (transform.position, Vector3.down, out fhit, Mathf.Infinity, layerMask);

		//Adding our own gravity
		Rb.AddForceAtPosition (Vector3.up * -5 * Mathf.Min(fhit.distance, 270), transform.position);
		//Adding thrust upward
		Rb.AddForceAtPosition (transform.up * (7500 / fhit.distance), transform.position);
		//Adding a dampening force
		Rb.AddForceAtPosition (Vector3.up * -2.5f * Rb.velocity.y, transform.transform.position);
	}
}
