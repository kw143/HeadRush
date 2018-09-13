using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
	public float aim;
	public GameObject bullet;
	private GameObject[] bulletPool;
	private int bulletNum = 0;
	private int maxBullets = 150;

	public GameObject vehicle;
	public GameObject bulletSpawn1;

	public GameObject bulletSpawn2;

	private float cooldown = 0f;
	public GameObject gmHolder;
	private GameManager gm;

	public float curRotation = 0f;
	public float vehicleRotation = 0f;
	private float prevAim;
	// Use this for initialization
	void Start () {

		gm = gmHolder.GetComponent<GameManager> ();
		bulletPool = new GameObject[maxBullets];
		for (int i = 0; i < maxBullets; i++) {
			bulletPool [i] = Instantiate (bullet);
			bulletPool [i].transform.position = new Vector3 (0, -99999, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;
		prevAim = aim;
		if (gm.Xbox_One_Controller) {
			aim = Input.GetAxis ("Aim");
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			aim = -1;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			aim = 1;
		} else {
			aim = 0;
		}
		vehicleRotation = vehicle.transform.eulerAngles.y;
		curRotation = transform.eulerAngles.y - vehicleRotation;
		if (curRotation < 0) {
			curRotation += 360;
		}
		if (Mathf.Abs (aim) == 1 && (prevAim == 0 || prevAim == aim) 
				&& ((curRotation >= 90 && curRotation <= 270)
				||  (curRotation > 0))) {
			if (curRotation <= 112 && curRotation >= 110) {
				if (aim > 0) {
					transform.Rotate (transform.up * Time.deltaTime * aim * 50);
				}
			} else if (curRotation <= 252 && curRotation >= 250) {
				if (aim < 0) {
					transform.Rotate (transform.up * Time.deltaTime * aim * 50);
				}
			} else {
				transform.Rotate (transform.up * Time.deltaTime * aim * 50);
			}

		} /*else if (Mathf.Abs (transform.eulerAngles.y) >= vehicle.transform.eulerAngles.y + 250) {
			transform.rotation = Quaternion.Euler (vehicle.transform.rotation.x, vehicle.transform.eulerAngles.y + 249, transform.rotation.z);
		} else if (Mathf.Abs (transform.eulerAngles.y) <= vehicle.transform.eulerAngles.y + 90) {
			transform.rotation = Quaternion.Euler (transform.rotation.x, vehicle.transform.eulerAngles.y + 91, transform.rotation.z);
		}*/
		if ((Input.GetAxis ("P2 Fire") >= .9 || Input.GetKey (KeyCode.UpArrow)) && cooldown <= 0) {
			if (bulletNum % 2 == 0) {
				bulletPool [bulletNum % maxBullets].GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
				bulletPool [bulletNum % maxBullets].transform.position = bulletSpawn1.transform.position;
				bulletPool [bulletNum % maxBullets].transform.rotation = bulletSpawn1.transform.rotation;
			} else {
				bulletPool [bulletNum % maxBullets].GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
				bulletPool [bulletNum % maxBullets].transform.position = bulletSpawn2.transform.position;
				bulletPool [bulletNum % maxBullets].transform.rotation = bulletSpawn2.transform.rotation;
			}
			bulletNum++;
			cooldown = .3f;
		}

	}
}
