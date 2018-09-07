using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
	public float aim;
	public GameObject bullet;
	private GameObject[] bulletPool;
	private int bulletNum = 0;
	private int maxBullets = 150;
	public GameObject bulletSpawn1;
	public GameObject bulletSpawn2;
	private float cooldown = 0f;
	public GameObject gmHolder;
	private GameManager gm;

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
		if (Mathf.Abs (aim) == 1 && (prevAim == 0 || prevAim == aim) && (Mathf.Abs (transform.eulerAngles.y) < 230 && Mathf.Abs (transform.eulerAngles.y) > 130)) {
			transform.Rotate (transform.up * Time.deltaTime * aim * 50);
		} else if (Mathf.Abs (transform.eulerAngles.y) >= 230) {
			transform.rotation = Quaternion.Euler (0, 229,0);
		} else if (Mathf.Abs (transform.eulerAngles.y) <= 130) {
			transform.rotation = Quaternion.Euler (0, 131,0);
		}
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
