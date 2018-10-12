using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chase : EnemyVehicleController
{

    public Transform vehicle;
    private GameObject player;
    int MoveSpeed = 21;
    int MaxDist = 10;
    int MinDist = 5;
    private float startingHealth = 10f;




    void Start()
    {
        player = GameObject.Find("Vehicle");
        vehicle = player.GetComponent<Transform>();
        base.Health = startingHealth;
        base.Rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        base.Update();
        transform.LookAt(vehicle);

        if (Vector3.Distance(transform.position, vehicle.position) >= MinDist)
        {

            //transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            this.MoveZDir(MoveSpeed, 1);




            if (Vector3.Distance(transform.position, vehicle.position) <= MaxDist)
            {
                //Shoot at here or something
            }

        }
    }
}