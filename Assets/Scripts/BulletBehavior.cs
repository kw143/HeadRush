using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {
	private Rigidbody rb;
	private float damage = 10f;



	public float Damage {
		get {
			return damage;
		}
	}


	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rb.velocity.z < 100) {
			rb.AddForce (transform.up * 100000 *Time.deltaTime);
		}
	}
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Obstacle") {
			Destroy (col.gameObject);
		} else if (col.gameObject.tag == "Enemy") {
			GameManager.addScore (5);
		}
	}
}
